using System.Collections.Generic;
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
    }

    public class RoomService : AppServiceBase<IRoomRepository>, IRoomService
    {
        public RoomService(IRoomRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        { }

        public async Task<int> CreateAsync(RoomCreateCommand command)
        {
            var room = _mapper.Map<Room>(command);
            var createdRoom = await _repository.CreateAsync(room);

            return await CommitAsync() > 0 ? createdRoom.ID : 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var room = await _repository.SingleOrDefaultAsync(x => x.ID == id);
            Guard.Against(room, ErrorType.NotFound);

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

            var usernameCount = await _repository.CountAsync(x => x.Name.Equals(command.Name));
            Guard.Against(usernameCount > 1, ErrorType.NotFound);

            room = _mapper.Map<Room>(command);

            _repository.Update(room);

            return await CommitAsync() > 0;
        }
    }
}
