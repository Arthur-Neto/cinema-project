using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theater.Domain.MoviesModule;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories.MoviesModule
{
    public class MovieRepository : GenericRepositoryBase<Movie, int>, IMovieRepository
    {
        public MovieRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<int> CountAsync(Expression<Func<Movie, bool>> expression)
        {
            return GenericRepository.CountAsync(expression);
        }

        public Task<Movie> CreateAsync(Movie movie)
        {
            return GenericRepository.CreateAsync(movie);
        }

        public Task DeleteAsync(int key)
        {
            return GenericRepository.DeleteAsync(key);
        }

        public Task<IEnumerable<Movie>> RetrieveAllAsync(params Expression<Func<Movie, object>>[] includeExpression)
        {
            return GenericRepository.RetrieveAllAsync(includeExpression);
        }

        public async Task<IEnumerable<Movie>> RetrieveMoviesWithSessionsAndRooms()
        {
            return await GenericRepository.Context.Set<Movie>().Include(p => p.Sessions).ThenInclude(p => p.Room).ToListAsync();
        }

        public Task<Movie> SingleOrDefaultAsync(Expression<Func<Movie, bool>> expression, bool tracking = true, params Expression<Func<Movie, object>>[] includeExpression)
        {
            return GenericRepository.SingleOrDefaultAsync(expression, tracking, includeExpression);
        }

        public void Update(Movie movie)
        {
            GenericRepository.Update(movie);
        }
    }
}
