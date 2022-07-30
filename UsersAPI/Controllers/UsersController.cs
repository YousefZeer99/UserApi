using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UsersAPI.Model;
using UsersAPI.Repos;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        INewUserRepo _userService;
        public UsersController(INewUserRepo service)
        {
            _userService = service;
        }

        // for part 2 from the task i will pass the role as a parameter with the header

        [HttpGet]
        public IActionResult GetAllUsers([FromHeader] string role)
        {
            if (role == "Admin") {
                var users = _userService.Get();
                if (users == null)
                    return NotFound();
                return Ok(users);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetId(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut]
        public IActionResult SaveUser(User user)
        {
            var model=_userService.Update(user);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Createuser(User user)
        {
            _userService.Add(user);
            return Ok(); 

        }


        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
