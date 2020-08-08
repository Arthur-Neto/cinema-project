using System.Collections.Generic;
using System.Threading.Tasks;
using Theater.Domain.RoomsModule;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories.RoomsModule
{
    public class RoomRepository : GenericRepositoryBase<Room, int>, IRoomRepository
    {
        public RoomRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<IEnumerable<Room>> RetrieveAllAsync()
        {
            return GenericRepository.RetrieveAllAsync();
        }
    }
}
