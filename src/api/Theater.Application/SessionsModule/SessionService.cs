using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    }

    public class SessionService : AppServiceBase<ISessionRepository>, ISessionService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMovieRepository _movieRepository;

        public SessionService(
            IRoomRepository roomRepository,
            IMovieRepository movieRepository,
            ISessionRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        {
            _roomRepository = roomRepository;
            _movieRepository = movieRepository;
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
            var sessions = await _repository.RetrieveAllAsync(p => p.Movie, p => p.Room);

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
    }
}
