using System.Collections.Generic;
using System.Threading.Tasks;
using Theater.Domain.UsersModule;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories.UsersModule
{
    public class UserRepository : GenericRepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<User> CreateAsync(User user)
        {
            return GenericRepository.CreateAsync(user);
        }

        public Task DeleteAsync(int id)
        {
            return GenericRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<User>> RetrieveAllAsync()
        {
            return GenericRepository.RetrieveAllAsync();
        }

        public Task<User> RetrieveByIDAsync(int id)
        {
            return GenericRepository.RetrieveByIDAsync(id);
        }

        public void Update(User user)
        {
            GenericRepository.Update(user);
        }
    }
}
