using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Banking.Appication.Interfaces;
using MicroRabbit.Banking.Appication.Models;
using MicroRabbit.Banking.Domain.Commands;
using MicroRabbit.Banking.Domain.Interfaces;
using MicroRabbit.Banking.Domain.Models;
using MicroRabbit.Domain.Core.Bus;

namespace MicroRabbit.Banking.Appication.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _eventBus;

        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }

        public IEnumerable<Account> GetAccountsAsync(CancellationToken cancellationToken) => _accountRepository.GetAccountsAsync(cancellationToken);
        
        public void TransferAsync(AccountTransfer accountTransfer, CancellationToken cancellationToken)
        {
            var createTransferCommand = new CreateTransferCommand(accountTransfer.FromAccount, accountTransfer.ToAccount, accountTransfer.TransferAmmount);
            _eventBus.SendCommand(createTransferCommand);
        }
    }
}