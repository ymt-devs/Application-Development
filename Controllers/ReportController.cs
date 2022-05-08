using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupCoursework.Data;
using GroupCoursework.ViewModel;
using GroupCoursework.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupCoursework.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Number 5 and 6
        public IActionResult StockListReport([FromQuery] string name = "")
        {
            List<ProductStockViewModel> lstData = new List<ProductStockViewModel>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                if (name == "")
                {
                    command.CommandText = "SELECT p.Id as ProductId,p.ProductName,ps.Quantity from Product p inner join ProductStock ps on p.Id=ps.ProductId";
                    
                }
                else
                {
                    command.CommandText = "SELECT p.Id as ProductId,p.ProductName,ps.Quantity from Product p inner join ProductStock ps on p.Id=ps.ProductId WHERE p.productName = '" + name + "'"; 
                }
                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {

                    ProductStockViewModel data;
                    while (result.Read())
                    {
                        data = new ProductStockViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity =result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }
            return View(lstData);
        }

        //Number 8
        public IActionResult PurchaseHistoryReport([FromQuery] string name = "")
        {
            List<PurchaseHistoryViewModel> lstData = new List<PurchaseHistoryViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                if (name == "")
                {
                    command.CommandText = "SELECT p.ID as ProductID, p.ProductName, sd.Quantity, sd.Price from Sales s JOIN SalesDetail sd ON s.ID = sd.ID JOIN Product p on p.ID = sd.ProductID";
                }
                else
                {
                    command.CommandText = "SELECT p.ID as ProductID, p.ProductName, sd.Quantity, sd.Price from Sales s JOIN SalesDetail sd ON s.ID = sd.ID JOIN Product p on p.ID = sd.ProductID where s.customerID ='" + name + "' and s.SalesDate >= GetDate()-31";

                }
                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    PurchaseHistoryViewModel data;
                    while (result.Read())
                    {
                        data = new PurchaseHistoryViewModel();
                        data.ProductID = result.GetInt32(0);
                        data.ProductName = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        data.Price = result.GetInt32(3);
                        lstData.Add(data);
                    }
                }
            }

            return View(lstData);
        }

        //Number 10
        public IActionResult RemoveStockedItemsReport()
        {
            List<RemoveStockedItemsViewModel> lstData = new List<RemoveStockedItemsViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"SELECT p.ProductName, p.Description, ps.Quantity from ProductStock ps
                                        JOIN product p ON p.ID = ps.ProductID
                                        WHERE ps.Quantity <=10 AND p.ID NOT IN 
                                        (SELECT DISTINCT sd.ProductID from Sales s
                                        JOIN SalesDetail sd on sd.SalesID = s.ID
                                        WHERE s.SalesDate > GETDATE() - 365)
                                        ";

                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    RemoveStockedItemsViewModel data;
                    while (result.Read())
                    {
                        data = new RemoveStockedItemsViewModel();
                        data.ProductName = result.GetString(0);
                        data.Description = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }

            return View(lstData);
        }

        //Number 11
        public IActionResult OutOfStockItemsReport([FromQuery] string sort = "")
        {
            List<OutofStockViewModel> lstData = new List<OutofStockViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {

                if (sort == "name" || sort == "")
                {
                    command.CommandText = @"SELECT p.ID, p.ProductName, ps.Quantity FROM Product p 
                                        INNER JOIN ProductStock ps on p.ID=ps.ProductId
                                        WHERE ps.Quantity <10 Order By p.ProductName";
                }
                else if(sort=="quantity")
                {
                    command.CommandText = @"SELECT p.ID, p.ProductName, ps.Quantity FROM Product p 
                                        INNER JOIN ProductStock ps on p.ID=ps.ProductId
                                        WHERE ps.Quantity <10 Order By ps.Quantity Desc";
                }

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

        //Number 12
        public IActionResult InactiveCustomersReport()
        {
            List<InactiveCustomersViewModel> lstData = new List<InactiveCustomersViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"SELECT Name, Email, PhoneNo from Customer
                                        WHERE ID NOT IN
                                        (SELECT DISTINCT CustomerID FROM Sales 
                                        WHERE SalesDate > GETDATE()- 31)";

                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    InactiveCustomersViewModel data;
                    while (result.Read())
                    {
                        data = new InactiveCustomersViewModel();
                        data.CustomerName = result.GetString(0);
                        data.CustomerEmail = result.GetString(1);
                        data.CustomerPhone = result.GetString(2);
                        lstData.Add(data);
                    }
                }
            }

            return View(lstData);
        }
        //Number 13
        public IActionResult InactiveItemsReport()
        {
            List<InactiveItemsViewModel> lstData = new List<InactiveItemsViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"SELECT p.ProductName, p.Description, ps.Quantity from ProductStock ps
                                        JOIN product p ON p.ID = ps.ProductID
                                        WHERE ps.Quantity >=10 AND p.ID NOT IN 
                                        (SELECT DISTINCT sd.ProductID from Sales s
                                        JOIN SalesDetail sd on sd.SalesID = s.ID
                                        WHERE s.SalesDate > GETDATE() - 31)
                                        ";

                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    InactiveItemsViewModel data;
                    while (result.Read())
                    {
                        data = new InactiveItemsViewModel();
                        data.ProductName = result.GetString(0);
                        data.Description = result.GetString(1);
                        data.Quantity = result.GetInt32(2);
                        lstData.Add(data);
                    }
                }
            }

            return View(lstData);
        }
       

    }
}
