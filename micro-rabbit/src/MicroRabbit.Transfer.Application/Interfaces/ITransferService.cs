using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Transfer.Domain.Models;

namespace MicroRabbit.Transfer.Application.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTransferLogsAsync(CancellationToken cancellationToken);
    }
}