using System.Text.Json;

namespace Ficha10.Models
{
    public class JsonLoader
    {
      /* public static Employee LoadEmployeeJSON()
        {
            string employeeJsonFile = File.ReadAllText("Employee.json");
            Employee employee = JsonSerializer.Deserialize<Employee>(employeeJsonFile);
            return employee;
        }*/

        public static Employees LoadEmployeesJSON()
        {
            string employeesJsonFile = File.ReadAllText("Employees.json");
            Employees employees = JsonSerializer.Deserialize<Employees>(employeesJsonFile);
            return employees;
        }

        public static Characters LoadCharactersJSON()
        {
            string charactersJsonFile = File.ReadAllText("Employees.json");
            Characters characters = JsonSerializer.Deserialize<Characters>(charactersJsonFile);
            return characters;
        }
    }
}
