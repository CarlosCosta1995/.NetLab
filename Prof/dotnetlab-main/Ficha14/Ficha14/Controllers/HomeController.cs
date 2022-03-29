﻿using Ficha14.Models;
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
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(IConfiguration config, IJWTService tokenService, IUserService userService, IWebHostEnvironment hostEnvironment)
        {
            this.config = config;
            this.tokenService = tokenService;
            this.userService = userService;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Logout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LogoutUser()
        {
            HttpContext.Session.Remove("Token");
            return (RedirectToAction("Index"));
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel user, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(this.hostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(file.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                    return RedirectToAction("Image", new UserModel { Path = file.FileName });
                }

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

        
        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return (RedirectToAction("Error"));
            }

            var user = userService.Get(userModel.UserName, userModel.Password);
            var validUser = new UserViewModel { UserName = user.UserName, ID = user.ID, Role = user.Role, Email = user.Email, Path = user.Path};

            if (validUser != null)
            {
                string generatedToken = tokenService.GenerateToken(
                    config["Jwt:Key"].ToString(),
                    config["Jwt:Issuer"].ToString(),
                    config["Jwt:Audience"].ToString(),
                validUser);

                if (generatedToken != null)
                {
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

        [HttpGet]
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

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult ApiTest()
        {
            TestModel test = null;
            using (var client = new HttpClient())
            {
                var token = HttpContext.Session.GetString("Token");
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                
                // Add token to header 
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                //HTTP GET
                var responseTask = client.GetAsync("test");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var task = result.Content.ReadFromJsonAsync<TestModel>();
                    task.Wait();
                    test = task.Result;
                }
                else //web api sent error response 
                {
                    test = new TestModel();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(test);
        }

        /// <summary>
        /// Image insert
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Image(UserModel image)
        {
            return View(image);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ErrorImage()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost("FileUpload")]
        //public IActionResult Index(IFormFile file)
        //{
        //    string path = Path.Combine(this.hostEnvironment.WebRootPath, "images");
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    string fileName = Path.GetFileName(file.FileName);
        //    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //        return RedirectToAction("Image", new UserModel { Path = file.FileName });
        //    }

        //    return RedirectToAction("Error");
        //}

    }
}