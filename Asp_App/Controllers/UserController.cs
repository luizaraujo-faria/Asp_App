using Asp_App.DTOs;
using Asp_App.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Asp_App.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(CreateUserDTO newUser)
        {
            if (ModelState.IsValid)
            {
                _repository.SignUp(newUser);
            }

            return View();
        }
    }
}