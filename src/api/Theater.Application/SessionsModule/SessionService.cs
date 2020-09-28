using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Theater.Application.SessionsModule.Commands;
using Theater.Application.SessionsModule.Models;
using Theater.Domain.MoviesModule;
using Theater.Domain.RoomsModule;
using Theater.Domain.SessionsModule;
using Theater.Infra.Crosscutting.Exceptions;
using Theater.Infra.Crosscutting.Guards;

namespace Theater.Application.SessionsModule
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionModel>> RetrieveAllAsync();
        Task<int> CreateAsync(SessionCreateCommand command);
        Task<bool> UpdateAsync(SessionUpdateCommand command);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<OccupiedChairModel>> GetOccupiedChairsAsync();
        Task<bool> CreateOccupiedChairsAsync(OccupiedChairsCommand command);
    }

    public class SessionService : AppServiceBase<ISessionRepository>, ISessionService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IOccupiedChairRepository _occupiedChairRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(
            IHttpContextAccessor httpContextAccessor,
            IRoomRepository roomRepository,
            IMovieRepository movieRepository,
            IOccupiedChairRepository occupiedChairRepository,
            ISessionRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _roomRepository = roomRepository;
            _movieRepository = movieRepository;
            _occupiedChairRepository = occupiedChairRepository;
        }

        public async Task<int> CreateAsync(SessionCreateCommand command)
        {
            var room = await _roomRepository.SingleOrDefaultAsync(p => p.ID == command.RoomId);
            Guard.Against(room, ErrorType.NotFound);

            var movie = await _movieRepository.SingleOrDefaultAsync(p => p.ID == command.MovieId);
            Guard.Against(movie, ErrorType.NotFound);

            var session = _mapper.Map<Session>(command);
            session.MovieId = movie.ID;
            session.RoomId = room.ID;

            var createdSession = await _repository.CreateAsync(session);

            return await CommitAsync() > 0 ? createdSession.ID : 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var session = await _repository.SingleOrDefaultAsync(x => x.ID == id);
            Guard.Against(session, ErrorType.NotFound);

            var lessThanTenDaysToStart = (session.Date - DateTime.UtcNow).TotalDays < 10;
            Guard.Against(lessThanTenDaysToStart, ErrorType.SessionLessThanTenDaysToStart);

            await _repository.DeleteAsync(id);

            return await CommitAsync() > 0;
        }

        public async Task<IEnumerable<SessionModel>> RetrieveAllAsync()
        {
            var sessions = await _repository.RetrieveAllAsync(null, p => p.Movie, p => p.Room);

            return _mapper.Map<IEnumerable<SessionModel>>(sessions);
        }

        public async Task<bool> UpdateAsync(SessionUpdateCommand command)
        {
            var session = await _repository.SingleOrDefaultAsync(x => x.ID == command.ID, tracking: false);
            Guard.Against(session, ErrorType.NotFound);

            session = _mapper.Map<Session>(command);

            _repository.Update(session);

            return await CommitAsync() > 0;
        }

        public async Task<IEnumerable<OccupiedChairModel>> GetOccupiedChairsAsync()
        {
            var occupiedChairs = await _occupiedChairRepository.RetrieveAllAsync();

            return _mapper.Map<IEnumerable<OccupiedChairModel>>(occupiedChairs);
        }

        public async Task<bool> CreateOccupiedChairsAsync(OccupiedChairsCommand command)
        {
            var session = await _repository.SingleOrDefaultAsync(p => p.ID == command.SessionId);
            Guard.Against(session, ErrorType.NotFound);

            foreach (var number in command.ChairsNumbers)
            {
                var occupiedChair = new OccupiedChair()
                {
                    Number = number,
                    SessionId = session.ID,
                    UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value)
                };

                await _occupiedChairRepository.CreateAsync(occupiedChair);
            }

            return await CommitAsync() > 0;
        }
    }
}
