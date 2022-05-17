using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDbContext _context; 
        
        public TransferRepository(TransferDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<TransferLog> GetTransferLogsAsync(CancellationToken cancellationToken) => _context.TransferLogs;
    }
}