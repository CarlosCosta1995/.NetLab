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

        public static List<Employee> LoadEmployeesJSON()
        {
            //cria uma lsita de Employees atavés do Array de Employee do Json
            string employeesJsonFile = File.ReadAllText("./JSONFile/Employees.json");
            return JsonSerializer.Deserialize<List<Employee>>(employeesJsonFile);
        }

        public static Characters LoadCharactersJSON()
        {
            string charactersJsonFile = File.ReadAllText("./JSONFile/Employees.json");
            Characters characters = JsonSerializer.Deserialize<Characters>(charactersJsonFile);
            return characters;
        }
    }
}
