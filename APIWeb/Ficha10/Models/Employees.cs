namespace Ficha10.Models
{
    public class Employees : IEmployees
    {
        //Lista tem de ter o mesmo nome que o Json(antes)
        private List<Employee> _employeesList;

        List<Employee> IEmployees.EmployeesList 
        {
            get => _employeesList;
            set => _employeesList = value;
        }
        public Employees()
        {
            //EmployeesList = new List<Employee>();
            _employeesList =  JsonLoader.LoadEmployeesJSON();
        }
    }
}
