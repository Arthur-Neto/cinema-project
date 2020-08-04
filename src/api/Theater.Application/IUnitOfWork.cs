using System;
using System.Threading.Tasks;

namespace Theater.Application
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
