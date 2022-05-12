namespace MicroRabbit.Banking.Domain.Commands
{
    public class CreateTransferCommand : TransferCommand
    {
        public CreateTransferCommand(int from, int to, decimal ammount)
        {
            FromAccount = from;
            ToAccount = to;
            Ammount = ammount;
        }
    }
}