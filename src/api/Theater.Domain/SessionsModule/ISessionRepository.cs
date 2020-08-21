namespace Theater.Domain.SessionsModule
{
    public interface ISessionRepository :
        IRetrieveAllRepository<Session>,
        ICreateRepository<Session>,
        IDeleteByIDRepository<Session, int>,
        IUpdateRepository<Session>,
        ISingleOrDefaultRepository<Session>,
        ICountRepository<Session>
    { }
}
