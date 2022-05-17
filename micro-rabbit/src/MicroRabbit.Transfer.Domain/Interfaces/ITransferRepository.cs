using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Domain.Interfaces
{
    public interface ITransferRepository
    {
        IEnumerable<TransferLog> GetTransferLogsAsync(CancellationToken cancellationToken);
    }
}