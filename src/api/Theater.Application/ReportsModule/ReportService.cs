using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Application.ReportsModule.Models;
using Theater.Domain.MoviesModule;

namespace Theater.Application.ReportsModule
{
    public interface IReportService
    {
        Task<IEnumerable<MoviesBillingModel>> GetMoviesBillingAsync();
    }

    public class ReportService : AppServiceBase<IMovieRepository>, IReportService
    {
        public ReportService(
            IMovieRepository repository,
            IMapper mapper
        ) : base(repository, mapper)
        { }

        public async Task<IEnumerable<MoviesBillingModel>> GetMoviesBillingAsync()
        {
            var movies = await _repository.RetrieveMoviesWithSessionsAndOccupiedChairs();

            var moviesBilling = new List<MoviesBillingModel>();

            var moviesGroupedById = movies.GroupBy(p => p.ID);
            foreach (var movieGrouped in moviesGroupedById)
            {
                var movieName = movieGrouped.Select(p => p.Title).First();
                var occupiedChairs = movieGrouped.SelectMany(p => p.Sessions).SelectMany(p => p.OccupiedChairs);
                var totalBilling = occupiedChairs.Count() * 10;

                moviesBilling.Add(new MoviesBillingModel() { MovieName = movieName, TotalBilling = totalBilling.ToString("C") });
            }

            return moviesBilling;
        }
    }
}
