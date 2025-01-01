using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "admin", Email = "admin@example.com" },
            new User { Id = 2, Username = "user", Email = "user@example.com" },
            new User { Id = 3, Username = "test", Email="test@example.com" }
        };
        [HttpGet]
        [Authorize]
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(users);
        }
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }
        [HttpPost]
        [Authorize]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            users.Add(user);
            return CreatedAtAction(nameof(GetUser),new {id = user.Id},user);
        }
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<User> UpdateUser(int id, [FromBody] User user)
        {
            var index = users.FindIndex(u => u.Id == id);
            if(index == -1) return NotFound();

            users[index] = user;

            return NoContent();
        }
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult DeleteUser(int id)
        {
            var index = users.FindIndex(u => u.Id == id);
            if (index == -1) return NotFound();
            users.RemoveAt(index);
            return NoContent();
        }
    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
