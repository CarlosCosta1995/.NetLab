namespace Ficha7
{
    public class Employees
    {
        //Atenção EmployeesList(aqui)  tem de ser igual ao do Ficheiro Json (Generico/Geral)
        public List<Employee> EmployeesList { get; set; }
        public Employees()
        {
            EmployeesList = new List<Employee>();
        }
    }
}
