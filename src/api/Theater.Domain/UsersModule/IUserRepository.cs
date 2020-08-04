namespace Theater.Domain.UsersModule
{
    public interface IUserRepository :
        ICreateRepository<User>,
        IDeleteByIDRepository<User, int>,
        IRetrieveAllRepository<User>,
        IRetrieveByIDRepository<User, int>,
        IUpdateRepository<User>
    { }
}
