namespace Theater.Domain.RoomsModule
{
    public interface IRoomRepository :
        IRetrieveAllRepository<Room>,
        ICreateRepository<Room>,
        IDeleteByIDRepository<Room, int>,
        IUpdateRepository<Room>,
        ISingleOrDefaultRepository<Room>,
        ICountRepository<Room>
    { }
}
