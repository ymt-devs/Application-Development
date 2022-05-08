using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GroupCoursework.Models;
using Microsoft.AspNetCore.Authorization;
using GroupCoursework.ViewModels;
using GroupCoursework.Data;
using Microsoft.EntityFrameworkCore;

namespace GroupCoursework.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<OutofStockViewModel> lstData = new List<OutofStockViewModel>();
            using(var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"SELECT p.ID, p.ProductName, ps.Quantity FROM Product p 
                                        INNER JOIN ProductStock ps on p.ID=ps.ProductId
                                        WHERE ps.Quantity <10 Order By p.ProductName";
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    OutofStockViewModel data;
                    while (result.Read())
                    {
                        data = new OutofStockViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
