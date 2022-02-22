using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ficha10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //criar uma instancia dos Employees
        private readonly Models.Employees emps;

        //Atribuir o valor do objecto atraves do construtor
        public EmployeesController()
        {
            //Como metodo é estatico e nao precisa de manipular nada, acessado atraves da Class
            //Caso contrario tem de ser instanciado e nao pode ser estatico, acessado atraves do Objecto de instancia
            this.emps = Models.JsonLoader.LoadEmployeesJSON();
        }

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


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Employees)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
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

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
