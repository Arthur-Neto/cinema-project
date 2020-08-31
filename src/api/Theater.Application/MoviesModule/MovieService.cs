using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Application.MoviesModule.Commands;
using Theater.Application.MoviesModule.Models;
using Theater.Application.SessionsModule.Models;
using Theater.Domain.MoviesModule;
using Theater.Infra.Crosscutting.Exceptions;
using Theater.Infra.Crosscutting.Guards;

namespace Theater.Application.MoviesModule
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieModel>> RetrieveAllAsync();
        Task<IEnumerable<MovieDashboardModel>> RetrieveMoviesDashboardAsync(DateTime date);


        Task<int> CreateAsync(MovieCreateCommand command);
        Task<bool> UpdateAsync(MovieUpdateCommand command);
        Task<bool> DeleteAsync(int id);
    }

    public class MovieService : AppServiceBase<IMovieRepository>, IMovieService
    {
        public MovieService(IMovieRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        { }

        public async Task<int> CreateAsync(MovieCreateCommand command)
        {
            var movie = _mapper.Map<Movie>(command);
            var createdMovie = await _repository.CreateAsync(movie);

            return await CommitAsync() > 0 ? createdMovie.ID : 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _repository.SingleOrDefaultAsync(x => x.ID == id, true, p => p.Sessions);
            Guard.Against(movie, ErrorType.NotFound);

            await _repository.DeleteAsync(id);

            return await CommitAsync() > 0;
        }

        public async Task<IEnumerable<MovieModel>> RetrieveAllAsync()
        {
            var movies = await _repository.RetrieveAllAsync();

            var movieModels = new List<MovieModel>();
            foreach (var movie in movies)
            {
                var movieModel = _mapper.Map<MovieModel>(movie);
                movieModel.Duration = FormatDuration(movie.Duration);
                movieModels.Add(movieModel);
            }

            return movieModels;
        }

        public async Task<IEnumerable<MovieDashboardModel>> RetrieveMoviesDashboardAsync(DateTime date)
        {
            var movies = await _repository.RetrieveMoviesWithSessionsAndRooms();

            var movieModels = new List<MovieDashboardModel>();
            foreach (var movie in movies)
            {
                var movieModel = _mapper.Map<MovieDashboardModel>(movie);
                movieModel.Sessions = new List<SessionDashboardModel>();
                movieModel.Duration = FormatDuration(movie.Duration);

                var sessionsGroupByRoom = movie.Sessions.GroupBy(p => p.RoomId);
                var sessionsGroupByDate = movie.Sessions.GroupBy(p => p.Date);
                foreach (var sessionGroupByRoom in sessionsGroupByRoom)
                {
                    var sessionID = sessionGroupByRoom.Select(p => p.ID).First();
                    var roomID = sessionGroupByRoom.Key;
                    var roomName = sessionGroupByRoom.Select(p => p.Room.Name).First();
                    var dates = sessionGroupByRoom
                        .Select(p => p.Date)
                        .Intersect(sessionsGroupByDate
                            .Select(p => p.Key))
                        .Where(p => p.Date == date.Date);

                    if (dates.Count() > 0)
                    {
                        movieModel.Sessions.Add(new SessionDashboardModel() { ID = sessionID, RoomID = roomID, RoomName = roomName, StartTimes = dates });
                    }
                }

                if (movieModel.Sessions.Count > 0)
                {
                    movieModels.Add(movieModel);
                }
            }

            return movieModels;
        }

        public async Task<bool> UpdateAsync(MovieUpdateCommand command)
        {
            var movie = await _repository.SingleOrDefaultAsync(x => x.ID == command.ID, tracking: false);
            Guard.Against(movie, ErrorType.NotFound);

            var titleCount = await _repository.CountAsync(x => x.Title.Equals(command.Title));
            Guard.Against(titleCount > 1, ErrorType.Duplicating);

            movie = _mapper.Map<Movie>(command);

            _repository.Update(movie);

            return await CommitAsync() > 0;
        }

        private string FormatDuration(string duration)
        {
            var spplitedDuration = duration.Split(":");
            var sb = new StringBuilder();
            sb.Append(spplitedDuration[0]);
            sb.Append("h");
            sb.Append(" ");
            sb.Append(spplitedDuration[1]);
            sb.Append("m");
            return sb.ToString();
        }
    }
}
