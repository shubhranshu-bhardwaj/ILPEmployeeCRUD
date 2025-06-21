// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.ILP2025.EmployeeCRUD.Repositores;
// using Microsoft.ILP2025.EmployeeCRUD.Servcies;

// namespace Microsoft.ILP2025.EmployeeCRUD.Web.Controllers
// {
//     public class EmployeeController : Controller
//     {
//         public IEmployeeService employeeService { get; set; }

//         public EmployeeController(IEmployeeService employeeService)
//         {
//             this.employeeService = employeeService;
//         }

//         // GET: EmployeeController
//         public async Task<ActionResult> Index()
//         {
//             var employees = await this.employeeService.GetAllEmployees();
//             return View(employees);
//         }

//         // GET: EmployeeController/Details/5
//         public async Task<ActionResult> Details(int id)
//         {
//             var employee = await this.employeeService.GetEmployee(id);
//             return View(employee);
//         }      
//     }
// }

using Microsoft.AspNetCore.Mvc;
using Microsoft.ILP2025.EmployeeCRUD.Entities;
using Microsoft.ILP2025.EmployeeCRUD.Servcies;

namespace Microsoft.ILP2025.EmployeeCRUD.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            var employees = await employeeService.GetAllEmployees();
            return View(employees);
        }

        // GET: EmployeeController/Details/
        // public async Task<ActionResult> Details(int id)
        // {
        //     var employee = await employeeService.GetEmployee(id);
        //     return View(employee);
        // }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeEntity employee)
        {
            try
            {
                await employeeService.CreateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/
        public async Task<ActionResult> Edit(int id)
        {
            var employee = await employeeService.GetEmployee(id);
            return View(employee);
        }

        // POST: EmployeeController/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeEntity employee)
        {
            try
            {
                await employeeService.UpdateEmployee(employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await employeeService.GetEmployee(id);
            return View(employee);
        }

        // POST: EmployeeController/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await employeeService.DeleteEmployee(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
