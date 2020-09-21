using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Application.RoomsModule.Commands;
using Theater.Application.RoomsModule.Models;
using Theater.Domain.RoomsModule;
using Theater.Infra.Crosscutting.Exceptions;
using Theater.Infra.Crosscutting.Guards;

namespace Theater.Application.RoomsModule
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomModel>> RetrieveAllAsync();
        Task<int> CreateAsync(RoomCreateCommand command);
        Task<bool> UpdateAsync(RoomUpdateCommand command);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RoomModel>> AvailableRoomsAsync(AvailableRoomsCommand command);
    }

    public class RoomService : AppServiceBase<IRoomRepository>, IRoomService
    {
        public RoomService(IRoomRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        { }

        public async Task<int> CreateAsync(RoomCreateCommand command)
        {
            var roomsCountByName = await _repository.CountAsync(x => x.Name.Equals(command.Name));
            Guard.Against(roomsCountByName > 0, ErrorType.Duplicating);

            var room = _mapper.Map<Room>(command);
            var createdRoom = await _repository.CreateAsync(room);

            return await CommitAsync() > 0 ? createdRoom.ID : 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var room = await _repository.SingleOrDefaultAsync(x => x.ID == id, true, p => p.Sessions);
            Guard.Against(room, ErrorType.NotFound);
            Guard.Against(room.Sessions.Count() > 0, ErrorType.RoomWithSession);

            await _repository.DeleteAsync(id);

            return await CommitAsync() > 0;
        }

        public async Task<IEnumerable<RoomModel>> RetrieveAllAsync()
        {
            var rooms = await _repository.RetrieveAllAsync();

            return _mapper.Map<IEnumerable<RoomModel>>(rooms);
        }

        public async Task<bool> UpdateAsync(RoomUpdateCommand command)
        {
            var room = await _repository.SingleOrDefaultAsync(x => x.ID == command.ID, tracking: false);
            Guard.Against(room, ErrorType.NotFound);

            var roomsCountByName = await _repository.CountAsync(x => x.Name.Equals(command.Name) && x.ID != command.ID);
            Guard.Against(roomsCountByName > 0, ErrorType.Duplicating);

            room = _mapper.Map<Room>(command);

            _repository.Update(room);

            return await CommitAsync() > 0;
        }

        public async Task<IEnumerable<RoomModel>> AvailableRoomsAsync(AvailableRoomsCommand command)
        {
            var rooms = await _repository.RetrieveAllAsync(p => p.Sessions);

            var roomsModels = new List<RoomModel>();
            foreach (var room in rooms)
            {
                if (room.Sessions.Count() == 0 || room.Sessions.Select(p => IsDateOverlaping(command.MovieDuration, p.Date, command.Date)).All(p => p == false))
                {
                    roomsModels.Add(_mapper.Map<RoomModel>(room));
                }
            }

            return roomsModels;
        }

        private bool IsDateOverlaping(string movieDuration, DateTimeOffset sessionDate, DateTimeOffset selectedDate)
        {
            var splittedDuration = movieDuration.Split(":");
            var durationOnMinutes = int.Parse(splittedDuration[0]) * 60 + int.Parse(splittedDuration[1]);

            var sessionStartDate = sessionDate;
            var sessionEndDate = sessionDate.AddMinutes(durationOnMinutes);

            var selectedStartDate = selectedDate;
            var selectedEndDate = selectedDate.AddMinutes(durationOnMinutes);

            return (sessionStartDate <= selectedEndDate) && (sessionEndDate >= selectedStartDate);
        }
    }
}
