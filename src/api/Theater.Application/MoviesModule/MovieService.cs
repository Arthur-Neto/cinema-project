using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Application.MoviesModule.Commands;
using Theater.Application.MoviesModule.Models;
using Theater.Domain.MoviesModule;
using Theater.Infra.Crosscutting.Exceptions;
using Theater.Infra.Crosscutting.Guards;

namespace Theater.Application.MoviesModule
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieModel>> RetrieveAllAsync();
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
            var movie = await _repository.SingleOrDefaultAsync(x => x.ID == id);
            Guard.Against(movie, ErrorType.NotFound);

            await _repository.DeleteAsync(id);

            return await CommitAsync() > 0;
        }

        public async Task<IEnumerable<MovieModel>> RetrieveAllAsync()
        {
            var rooms = await _repository.RetrieveAllAsync();

            return _mapper.Map<IEnumerable<MovieModel>>(rooms);
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
    }
}
