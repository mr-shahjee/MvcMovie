using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using DataAccessLayer;
using Models;
using Microsoft.AspNetCore.SignalR;

namespace MvcMovie.Controllers;

public class HomeController : Controller
{
    private readonly IHubContext<StudentHub> _hubContext;
    private readonly EmployeesDAL empDAL;
    public HomeController(IHubContext<StudentHub> hubContext)
    {
        empDAL = new EmployeesDAL();
        _hubContext = hubContext;
    }

    public IActionResult Index()
    {
        List<Employees> emps = empDAL.getAllEmployees();
        return View(emps);
    }
    public IActionResult Create()
    { 
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employees emp)
    {
        try {
            // Verify if emp.Id is correctly set
            empDAL.AddEmployee(emp);
            if (emp.Id == 0)
            {
                // Handle the error appropriately, perhaps log it
                throw new Exception("Failed to retrieve the newly created employee Id.");
            }
            await _hubContext.Clients.All.SendAsync("ReceiveStudentRecord", emp);
             
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
        return View();
        }
    }
    public IActionResult Edit(int? id)
    {
        Employees emp = empDAL.getEmployeeById(id);
        return View(emp);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Employees emp)
    {
        try
        {
            empDAL.UpdateEmployee(emp);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return View();
        }
    }
    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Details(int? id)
    {
        Employees emp = empDAL.getEmployeeById(id);
        return View(emp);
    }
    public IActionResult Delete(int? id)
    {
        Employees emp = empDAL.getEmployeeById(id);
        return View(emp);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Employees emp)
    {
        try
        {
            empDAL.DeleteEmployee(emp.Id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            return View();
        }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
