using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Theater.Domain.SessionsModule;
using Theater.Infra.Data.EF.Context;

namespace Theater.Infra.Data.EF.Repositories.SessionsModule
{
    public class SessionRepository : GenericRepositoryBase<Session, int>, ISessionRepository
    {
        public SessionRepository(IDatabaseContext context)
            : base(context)
        { }

        public Task<int> CountAsync(Expression<Func<Session, bool>> expression)
        {
            return GenericRepository.CountAsync(expression);
        }

        public Task<Session> CreateAsync(Session session)
        {
            return GenericRepository.CreateAsync(session);
        }

        public Task DeleteAsync(int key)
        {
            return GenericRepository.DeleteAsync(key);
        }

        public Task<IEnumerable<Session>> RetrieveAllAsync(params Expression<Func<Session, object>>[] includeExpression)
        {
            return GenericRepository.RetrieveAllAsync(includeExpression);
        }

        public Task<Session> SingleOrDefaultAsync(Expression<Func<Session, bool>> expression, bool tracking = true, params Expression<Func<Session, object>>[] includeExpression)
        {
            return GenericRepository.SingleOrDefaultAsync(expression, tracking, includeExpression);
        }

        public void Update(Session session)
        {
            GenericRepository.Update(session);
        }
    }
}
