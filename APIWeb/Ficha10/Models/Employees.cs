namespace Ficha10.Models
{
    public class Employees
    {
        //Lista tem de ter o mesmo nome que o Json
        public List<Employee> EmployeesList { get; set; }
        public Employees()
        {
            EmployeesList = new List<Employee>();
        }
    }
}
