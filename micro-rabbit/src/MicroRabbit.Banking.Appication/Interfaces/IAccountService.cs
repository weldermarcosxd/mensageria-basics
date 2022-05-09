using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Appication.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccountsAsync(CancellationToken cancellationToken); 
    }
}