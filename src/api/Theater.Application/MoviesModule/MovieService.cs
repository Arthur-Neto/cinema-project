using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Application.FilesManagerModule;
using Theater.Application.MoviesModule.Commands;
using Theater.Application.MoviesModule.Models;
using Theater.Application.RoomsModule.Models;
using Theater.Application.SessionsModule.Models;
using Theater.Domain.MoviesModule;
using Theater.Infra.Crosscutting.Exceptions;
using Theater.Infra.Crosscutting.Guards;

namespace Theater.Application.MoviesModule
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieModel>> RetrieveAllAsync();
        Task<IEnumerable<MovieDashboardModel>> RetrieveMoviesDashboardAsync(DateTimeOffset date);


        Task<int> CreateAsync(MovieCreateCommand command);
        Task<bool> UpdateAsync(MovieUpdateCommand command);
        Task<bool> UpdateCoverAsync(UpdateCoverCommand command);
        Task<bool> DeleteAsync(int id);
    }

    public class MovieService : AppServiceBase<IMovieRepository>, IMovieService
    {
        private readonly IFileManagerService _fileManagerService;

        public MovieService(
            IFileManagerService fileManagerService,
            IMovieRepository repository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        {
            _fileManagerService = fileManagerService;
        }

        public async Task<int> CreateAsync(MovieCreateCommand command)
        {
            var moviesCountByName = await _repository.CountAsync(x => x.Title.Equals(command.Title));
            Guard.Against(moviesCountByName > 0, ErrorType.Duplicating);

            var movie = _mapper.Map<Movie>(command);
            movie.ImagePath = $"{Environment.CurrentDirectory}\\wwwroot\\movies-imgs\\default.png";

            var createdMovie = await _repository.CreateAsync(movie);

            return await CommitAsync() > 0 ? createdMovie.ID : 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _repository.SingleOrDefaultAsync(x => x.ID == id, true, p => p.Sessions);
            Guard.Against(movie, ErrorType.NotFound);

            _fileManagerService.RemoveCoverImage(id);

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
                movieModels.Add(movieModel);
            }

            return movieModels;
        }

        public async Task<IEnumerable<MovieDashboardModel>> RetrieveMoviesDashboardAsync(DateTimeOffset date)
        {
            var movies = await _repository.RetrieveMoviesWithSessionsAndRooms();

            var movieModels = new List<MovieDashboardModel>();
            foreach (var movie in movies)
            {
                var movieModel = _mapper.Map<MovieDashboardModel>(movie);
                movieModel.Rooms = new List<RoomDashboardModel>();

                var sessionsGroupByRoom = movie.Sessions.Where(p => p.Date.Date == date.ToLocalTime().Date).GroupBy(p => p.RoomId);
                foreach (var sessionGroupByRoom in sessionsGroupByRoom)
                {
                    var roomId = sessionGroupByRoom.Key;
                    var roomName = sessionGroupByRoom.Select(p => p.Room.Name).First();
                    var numberOfChairs = sessionGroupByRoom.Select(p => p.Room.NumberOfChairs).First();

                    var sessions = _mapper.Map<IEnumerable<SessionDashboardModel>>(sessionGroupByRoom.Select(p => p));

                    if (sessions.Any())
                    {
                        movieModel.Rooms.Add(new RoomDashboardModel() { ID = roomId, Name = roomName, NumberOfChairs = numberOfChairs, Sessions = sessions });
                    }
                }

                if (movieModel.Rooms.Any())
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

            var moviesCountByName = await _repository.CountAsync(x => x.Title.Equals(command.Title) && x.ID != command.ID);
            Guard.Against(moviesCountByName > 0, ErrorType.Duplicating);

            movie = _mapper.Map<Movie>(command);

            _repository.Update(movie);

            return await CommitAsync() > 0;
        }

        public async Task<bool> UpdateCoverAsync(UpdateCoverCommand command)
        {
            var movie = await _repository.SingleOrDefaultAsync(x => x.ID == command.ID, tracking: false);
            Guard.Against(movie, ErrorType.NotFound);

            var filePath = await _fileManagerService.CreateCoverImageAsync(command.Image, command.ID);

            movie.ImagePath = filePath;

            _repository.Update(movie);

            return await CommitAsync() > 0;
        }
    }
}
