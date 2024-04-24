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
    public IActionResult Create()
    { 
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employees emp)
    {
        try {
            empDAL.AddEmployee(emp);
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
