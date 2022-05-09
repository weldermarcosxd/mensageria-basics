using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Banking.Appication.Interfaces;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;

namespace MicroRabbit.Banking.Appication.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IEnumerable<Account> GetAccountsAsync(CancellationToken cancellationToken) => _accountRepository.GetAccountsAsync(cancellationToken);
    }
}