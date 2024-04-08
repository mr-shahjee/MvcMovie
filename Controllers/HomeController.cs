using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using DataAccessLayer;
using Models;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{ 
    private readonly EmployeesDAL empDAL;
    public HomeController( )
    {
        empDAL = new EmployeesDAL();
    }

    public IActionResult Index()
    {
        List<Employees> emps = empDAL.getAllEmployees();
        return View(emps);
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
