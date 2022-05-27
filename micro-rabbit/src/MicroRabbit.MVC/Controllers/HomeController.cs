﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MicroRabbit.MVC.Models;
using MicroRabbit.MVC.Models.DTO;
using MicroRabbit.MVC.Services;

namespace MicroRabbit.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransferService _transferService;

        public HomeController(ILogger<HomeController> logger, ITransferService transferService)
        {
            _logger = logger;
            _transferService = transferService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferViewModel model)
        {
            var transferDto = new TransferDto()
            {
                FromAccount = model.FromAccount,
                ToAccount = model.ToAccount,
                TransferAmmount = model.TransferAmount
            };

            await _transferService.Transfer(transferDto);

            return View("Index");

        }
    }
}