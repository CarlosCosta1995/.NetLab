using Ficha10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ficha10.Controllers
{
    [Route("api/[controller]")]//Controller é sempre substituido pelo nome do controller (EmployeesController)
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //criar uma instancia dos Employees
        private readonly Models.Employees emps;

        //Atribuir o valor do objecto atraves do construtor
        public EmployeesController()
        {
            //Como metodo é estatico e nao precisa de manipular nada, acessado atraves da Class
            //Caso contrario tem de ser instanciado e nao pode ser estatico, acessado -atraves do Objecto de instancia
                this.emps = Models.JsonLoader.LoadEmployeesJSON();
        }

        /*-----------------------------
         * GET
         * ----------------------------*/

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Models.Employee> Get()
        {
            //IEnumerable generaliza para qualquer tipo de lista, array, linked list,...
            return emps.EmployeesList;
        }
        /*
        public IActionResult Get()
        {
            if (emps == null)
            {
                return NotFound("ID not found");
            }
            else
            {
                return Ok(emps.EmployeesList);
            }
        }*/


        /*-----------------------------
         * GET : ID
         * ----------------------------*/

        // GET api/<ValuesController>/5
        [HttpGet("employee/{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Employees))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEmployee(int id)
        {
            //? == Nullable (Se nao encontrar, sera null)
            Models.Employee? employee = emps.EmployeesList.Find(user => user.UserId == id);

            if (emps == null)
            {
                return NotFound($"ID: {id} not found!");
            }
            else
            {
                return Ok(employee);
            }
        }


        /*-----------------------------
         * POST (Need Dependency Injection)
         * ----------------------------*/

        // POST api/<ValuesController>
        [HttpPost("", Name = "Post")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Models.Employee))]
        public IActionResult Post(Employee p)
        {
            if (emps.EmployeesList.Count == 0)
            {
                p.UserId = 1;
                emps.EmployeesList.Add(p);
            }
            else
            {
                var lastEmp = emps.EmployeesList[emps.EmployeesList.Count - 1];
                p.UserId = lastEmp.UserId + 1;
                emps.EmployeesList.Add(p);
            }
            return Created("/Employees", p);
        }


        /*-----------------------------
         * PUT
         * ----------------------------*/

        // PUT api/<ValuesController>/5
        [HttpPut("{id}", Name = "Put")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Employees))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, Employee empPut)
        {
            Employee? employee = emps.EmployeesList.Find(emp => emp.UserId == id);

            if (employee == null)
            {
                //String format é um metodo de class != Metodo de instancia [str.indexof("o")]
                return NotFound($"ID: {id} not found!");
            }
            else
            {
                employee.JobTitle = empPut.JobTitle;
                employee.FirstName = empPut.FirstName;
                employee.LastName = empPut.LastName;
                employee.EmployeeCode = empPut.EmployeeCode;
                employee.Region = empPut.Region;
                employee.PhoneNumber = empPut.PhoneNumber;
                employee.EmailAddress = empPut.EmailAddress;
                return Ok(employee);
            }
        }


        /*-----------------------------
         * DELETE
         * ----------------------------*/

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Employees))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            int removed = emps.EmployeesList.RemoveAll(e => e.UserId == id);

            if (removed == 0)
            {
                //String format é um metodo de class != Metodo de instancia [str.indexof("o")]
                return NotFound("Id not found!");
            }
            else
            {
                return Ok(String.Format("ID: {0} deleted", id));
            }
        }

        /*-----------------------------
         * GET : REGION
         * ----------------------------*/

        // GET api/<ValuesController>/5
        [HttpGet("region/{region}", Name = "GetByRegion")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Employees))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByRegion(string region)
        {
            Employee? employees = emps.EmployeesList.Find(e => e.Region == region);
            if (employees == null)
            {
                return NotFound($"{region} not found.");
            }
            else
            {
                return Ok(employees);
            }
        }

        /*-----------------------------
         * DOWNLOAD
         * ----------------------------*/

        // GET api/<ValuesController>/5
        [HttpGet("download", Name = "GetDownload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Employees))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDownload()
        {
            string listEmps = JsonSerializer.Serialize<Employees>(emps);
            //namespace.Class.Function
            System.IO.File.WriteAllText("./EmployeesList.json", listEmps);

            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes("EmployeesList.json");
                //return File(bytes, "application/json");
                return File(bytes, "application/json", "EmployeesList.json");
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
