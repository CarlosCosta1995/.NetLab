using Ficha14.Models;
using Ficha14.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ficha14.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        private readonly IJWTService tokenService;
        private readonly IUserService userService;        

        public HomeController(IConfiguration config, IJWTService tokenService, IUserService userService)
        {
            this.config = config;
            this.tokenService = tokenService;
            this.userService = userService;
        }     

        public IActionResult Index()
        {
            return View();
        }
     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //================= Logn IN ===============
        //========== Add a view do user details =========
        public IActionResult UserDetails()
        {
            return View();
        }

        //========= add o endpoint view do user details (Criaçao do token) ===========
        [AllowAnonymous] //invocar sem estar Logged In
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(User userModel)
        {
            //Validação Username e password
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password)) 
            {
                return (RedirectToAction("Error"));
            }

            //Informaçao a ser enviada para a View Através do UserViewModel
            var user = userService.GetUser(userModel.UserName, userModel.Password); 
            var validUser = new UserViewModel { 
                UserName = userModel.UserName,
                ID = userModel.ID,
                Email = userModel.Email,
                Role = userModel.Role
            };
            if (validUser != null) //Se user for valido Gera um Token
            {
                string generatedToken = tokenService.GenerateToken(
                    config["Jwt:Key"].ToString(),
                    config["Jwt:Issuer"].ToString(),
                    config["Jwt:Audience"].ToString(),
                validUser);

                if (generatedToken != null)
                {
                    //Addicionamos o token a Sessao
                    HttpContext.Session.SetString("Token", generatedToken);
                    return RedirectToAction("UserDetails", validUser);
                }
                else
                {
                    return (RedirectToAction("Error"));
                }
            }
            else
            {
                return (RedirectToAction("Error"));
            }
        }

        //====== Validate Token for the session (Validate Existing Token) =========
        public IActionResult UserDetails(UserViewModel user)
        {
            string token = HttpContext.Session.GetString("Token");

            if (token == null)
            {
                return (RedirectToAction("Index"));
            }

            if (!tokenService.IsTokenValid(
                config["Jwt:Key"].ToString(),
                config["Jwt:Issuer"].ToString(),
                config["Jwt:Audience"].ToString(),
                token))
            {
                return (RedirectToAction("Index"));
            }

            ViewBag.Token = token;
            return View(user);
        }


        //===============  SignUP
        //Endpoint View
        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                var userExists = userService.FindByName(user.UserName);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");

                var newUser = userService.Create(user);
                if (newUser is not null)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Error));
            }
            else
            {
                return RedirectToAction(nameof(Error));
            }
        }
    }
}