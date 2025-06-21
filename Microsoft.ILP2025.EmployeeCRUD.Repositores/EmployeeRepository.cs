// using Microsoft.ILP2025.EmployeeCRUD.Entities;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
// {
//     public class EmployeeRepository : IEmployeeRepository
//     {
//         public async Task<List<EmployeeEntity>> GetAllEmployees()
//         {
//             return await Task.FromResult(this.GetEmployees());
//         }

//         public async Task<EmployeeEntity> GetEmployee(int id)
//         {
//             var employees = this.GetEmployees();

//             return await Task.FromResult(employees.FirstOrDefault(e => e.Id == id));
//         }

//         private List<EmployeeEntity> GetEmployees()
//         {
//             var employees = new List<EmployeeEntity>();

//             employees.Add(new EmployeeEntity { Id = 1, Name = "Pradip" });
//             employees.Add(new EmployeeEntity { Id = 2, Name = "Shrikanth" });

//             return employees;
//         }
//     }
// }


// using Microsoft.ILP2025.EmployeeCRUD.Entities;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
// {
//     public class EmployeeRepository : IEmployeeRepository
//     {
//         // In-memory list acting as a mock database
//         private static List<EmployeeEntity> employees = new List<EmployeeEntity>
//         {
//             new EmployeeEntity {
//                 Id = 1,
//                 Name = "Shubh",
//                 Email = "shubh@example.com",
//                 Departmet = "IT",
//                 Designation = "Developer",
//                 DateOfJoing = new DateTime(2023, 5, 8)
//                 },
//             new EmployeeEntity {
//                 Id = 2,
//                 Name = "Adarsh",
//                 Email = "adarsh@example.com",
//                 Departmet = "HR",
//                 Designation = "Manager",
//                 DateOfJoing = new DateTime(2023, 5, 8)
//                 }
//         };

//         // Get all employees
//         public async Task<List<EmployeeEntity>> GetAllEmployees()
//         {
//             return await Task.FromResult(employees);
//         }

//         // Get employee by ID
//         public async Task<EmployeeEntity> GetEmployee(int id)
//         {
//             var employee = employees.FirstOrDefault(e => e.Id == id);
//             return await Task.FromResult(employee);
//         }

//         // Create new employee
//         public async Task CreateEmployee(EmployeeEntity employee)
//         {
//             // Generate new ID
//             int newId = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
//             employee.Id = newId;
//             employees.Add(employee);
//             await Task.CompletedTask; // async compliance
//         }

//         // Update existing employee
//         public async Task UpdateEmployee(EmployeeEntity updatedEmployee)
//         {
//             var existingEmployee = employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
//             if (existingEmployee != null)
//             {
//                 existingEmployee.Name = updatedEmployee.Name;
//                 // Add more fields as needed
//             }

//             await Task.CompletedTask;
//         }

//         // Delete employee by ID
//         public async Task DeleteEmployee(int id)
//         {
//             var employeeToDelete = employees.FirstOrDefault(e => e.Id == id);
//             if (employeeToDelete != null)
//             {
//                 employees.Remove(employeeToDelete);
//             }

//             await Task.CompletedTask;
//         }
//     }
// }


using Microsoft.ILP2025.EmployeeCRUD.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // private readonly string filePath = "employee.json";
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "employee.json");


        private async Task<List<EmployeeEntity>> ReadFromFileAsync()
        {
            if (!File.Exists(filePath))
                return new List<EmployeeEntity>();

            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<EmployeeEntity>>(json) ?? new List<EmployeeEntity>();
        }

        private async Task WriteToFileAsync(List<EmployeeEntity> employees)
        {
            string json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {
            return await ReadFromFileAsync();
        }

        public async Task<EmployeeEntity> GetEmployee(int id)
        {
            var employees = await ReadFromFileAsync();
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public async Task CreateEmployee(EmployeeEntity employee)
        {
            var employees = await ReadFromFileAsync();
            employee.Id = employees.Any() ? employees.Max(e => e.Id) + 1 : 1;
            employees.Add(employee);
            await WriteToFileAsync(employees);
        }

        public async Task UpdateEmployee(EmployeeEntity updatedEmployee)
        {
            var employees = await ReadFromFileAsync();
            var index = employees.FindIndex(e => e.Id == updatedEmployee.Id);
            if (index != -1)
            {
                employees[index] = updatedEmployee;
                await WriteToFileAsync(employees);
            }
        }

        public async Task DeleteEmployee(int id)
        {
            var employees = await ReadFromFileAsync();
            var employeeToRemove = employees.FirstOrDefault(e => e.Id == id);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
                await WriteToFileAsync(employees);
            }
        }
    }
}
