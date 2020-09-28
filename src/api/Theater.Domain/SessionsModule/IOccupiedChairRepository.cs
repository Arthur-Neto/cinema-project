namespace Theater.Domain.SessionsModule
{
    public interface IOccupiedChairRepository :
        ICreateRepository<OccupiedChair>,
        ISingleOrDefaultRepository<OccupiedChair>,
        IRetrieveAllRepository<OccupiedChair>
    { }
}
