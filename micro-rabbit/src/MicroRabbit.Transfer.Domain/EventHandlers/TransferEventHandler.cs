using System.Threading;
using System.Threading.Tasks;
using MicroRabbit.Domain.Core.Bus;
using MicroRabbit.Transfer.Domain.Events;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;

        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public async Task Handle(TransferCreatedEvent @event, CancellationToken cancellationToken)
        {
            await _transferRepository.AddAsync(new TransferLog
            {
                FromAccount = @event.From, 
                ToAccount = @event.To, 
                TransferAmount = @event.Ammount
            }, cancellationToken);
        }
    }
}