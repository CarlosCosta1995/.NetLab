using Ficha10.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ficha10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly Characters chars;

        public CharactersController()
        {
            this.chars = JsonLoader.LoadCharactersJSON();
        }


        /*-----------------------------
         * GET
         * ----------------------------*/

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Character> Get()
        {
            return chars.CharactersList;
        }

        /*-----------------------------
         * GET : ID
         * ----------------------------*/

        // GET api/<ValuesController>/5
        [HttpGet("character/{id:int}", Name = "GetCharacter")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Characters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCharacter(int id)
        {
            Models.Character? toon = chars.CharactersList.Find(user => user.Id == id);

            if (chars == null)
            {
                return NotFound($"ID: {id} not found!");
            }
            else
            {
                return Ok(toon);
            }
        }

        /*-----------------------------
          * POST
          * ----------------------------*/

        // POST api/<ValuesController>
        [HttpPost("", Name = "Post")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Models.Character))]
        public IActionResult Post(Character toon)
        {
            if (chars.CharactersList.Count == 0)
            {
                toon.Id = 1;
                chars.CharactersList.Add(toon);
            }
            else
            {
                var lastToon = chars.CharactersList[chars.CharactersList.Count - 1];
                toon.Id = lastToon.Id + 1;
                chars.CharactersList.Add(toon);
            }
            return Created("/Characters", toon);
        }

        /*-----------------------------
                * PUT
                * ----------------------------*/

        // PUT api/<ValuesController>/5
        [HttpPut("{id}", Name = "Put")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Characters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, Character charPut)
        {
            Character? toon = chars.CharactersList.Find(toon => toon.Id == id);

            if (toon == null)
            {
                //String format é um metodo de class != Metodo de instancia [str.indexof("o")]
                return NotFound($"ID: {id} not found!");
            }
            else
            {
                toon.Name = charPut.Name;
                toon.Gender = charPut.Gender;
                toon.Homeworld = charPut.Homeworld;
                toon.Born = charPut.Born;
                toon.Jedi = charPut.Jedi;
                return Ok(toon);
            }
        }


        /*-----------------------------
         * DELETE
         * ----------------------------*/

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Characters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            int removed = chars.CharactersList.RemoveAll(toon => toon.Id == id);

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
         * GET : GENDER
         * ----------------------------*/

        // GET api/<ValuesController>/5
        [HttpGet("gender/{gender}", Name = "GetByGender")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Characters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByRegion(string gender)
        {
            List<Character>? toon = chars.CharactersList.FindAll(toon => toon.Gender == gender);
            if (toon.Count == 0)
            {
                return NotFound($"{gender} not found.");
            }
            else
            {
                return Ok(toon);
            }
        }


        /*-----------------------------
         * GET : JEDI
         * ----------------------------*/

        // GET api/<ValuesController>/5
        [HttpGet("jedi/{jedi}", Name = "GetByJedi")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Characters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByJedi(bool jedi)
        {
            List<Character>? toon = chars.CharactersList.FindAll(toon => toon.Jedi == jedi);
            if (toon.Count == 0)
            {
                return NotFound("Could not found a jedi.");
            }
            else
            {
                return Ok(toon);
            }
        }

        /*-----------------------------
         * DOWNLOAD
         * ----------------------------*/

        // GET api/<ValuesController>/5
        [HttpGet("download", Name = "GetDownload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.Characters))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDownload()
        {
            string listChars = JsonSerializer.Serialize<Characters>(chars);
            //namespace.Class.Function
            System.IO.File.WriteAllText("./CharactersList.json", listChars);

            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes("CharactersList.json");
                return File(bytes, "application/json", "CharactersList.json");
            }
            catch (FileNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
