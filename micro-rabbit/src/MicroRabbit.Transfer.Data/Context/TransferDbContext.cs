using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Transfer.Data.Context
{
    public class TransferDbContext : DbContext
    {
        public TransferDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}