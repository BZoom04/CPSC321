using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ThreeLayerDemoApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await employeeService.GetEmployeesAsync(); //this makes sure the business logic gets processed before returning the view
            return View(employees);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            await employeeService.AddEmployeeAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            await employeeService.UpdateEmployeeAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeDetails(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await employeeService.GetDetailsAsync(employee);
            return View(employee);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            await employeeService.GetDetailsAsync(employee);
            return View(employee);
        }
    }
}
