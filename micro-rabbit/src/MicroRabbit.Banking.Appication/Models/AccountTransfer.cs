namespace MicroRabbit.Banking.Appication.Models
{
    public class AccountTransfer
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public decimal TransferAmmount { get; set; }
    }
}