using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        Task AddAsync(TransferLog transferLog, CancellationToken cancellationToken);
        IEnumerable<TransferLog> GetTransferLogsAsync(CancellationToken cancellationToken);
    }
}