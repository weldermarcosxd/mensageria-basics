using System.Collections.Generic;
using System.Threading;
using MicroRabbit.Banking.Appication.Interfaces;
using MicroRabbit.Banking.Appication.Models;
using MicroRabbit.Banking.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MicroRabbit.Banking.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get(CancellationToken cancellationToken) =>
            Ok(_accountService.GetAccountsAsync(cancellationToken));

        [HttpPost]
        public IActionResult Post([FromBody] AccountTransfer accountTransfer, CancellationToken cancellationToken)
        {
            _accountService.TransferAsync(accountTransfer, cancellationToken);
            return Ok(accountTransfer);
        }
    }
}