using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Theater.Application.RoomsModule.Models;
using Theater.Domain.RoomsModule;

namespace Theater.Application.RoomsModule
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomModel>> RetrieveAllAsync();
    }

    public class RoomService : AppServiceBase<IRoomRepository>, IRoomService
    {
        public RoomService(IRoomRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(repository, mapper, unitOfWork)
        { }

        public async Task<IEnumerable<RoomModel>> RetrieveAllAsync()
        {
            var rooms = await _repository.RetrieveAllAsync();

            return _mapper.Map<IEnumerable<RoomModel>>(rooms);
        }
    }
}
