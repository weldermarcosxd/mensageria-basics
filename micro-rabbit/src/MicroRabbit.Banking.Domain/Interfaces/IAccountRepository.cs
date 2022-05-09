using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Domain.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccountsAsync(CancellationToken cancellationToken);
    }
}