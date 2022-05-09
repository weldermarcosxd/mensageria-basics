using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Banking.Data.Context;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _context; 
        
        public AccountRepository(BankingDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Account> GetAccountsAsync(CancellationToken cancellationToken) => _context.Accounts;
    }
}