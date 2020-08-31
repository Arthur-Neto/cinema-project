using System.Collections.Generic;
using System.Threading.Tasks;

namespace Theater.Domain.MoviesModule
{
    public interface IMovieRepository :
        IRetrieveAllRepository<Movie>,
        ICreateRepository<Movie>,
        IDeleteByIDRepository<Movie, int>,
        IUpdateRepository<Movie>,
        ISingleOrDefaultRepository<Movie>,
        ICountRepository<Movie>
    {
        Task<IEnumerable<Movie>> RetrieveMoviesWithSessionsAndRooms();
    }
}
