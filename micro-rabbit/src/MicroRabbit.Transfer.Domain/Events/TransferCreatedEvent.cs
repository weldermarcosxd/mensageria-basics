using MicroRabbit.Domain.Core.Events;

namespace MicroRabbit.Transfer.Domain.Events
{
    public class TransferCreatedEvent : Event
    {
        public int From { get; }
        public int To { get; }
        public decimal Ammount { get; }

        public TransferCreatedEvent(int from, int to, decimal ammount)
        {
            From = from;
            To = to;
            Ammount = ammount;
        }
    }
}