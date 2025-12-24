using Microsoft.AspNetCore.Mvc;
using assignment3.Models;
using System.Collections.Generic;
using System.Linq;

namespace assignment3.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private static List<User> users = new List<User>
		{
			new User { Id = 1, Name = "Menna", Email = "menna@example.com", Password = "7777", PhoneNumber = "01111111111" },
			new User { Id = 2, Name = "Lamar", Email = "lamar@example.com", Password = "3333", PhoneNumber = "02222222222" }
			
		};

		[HttpGet]
		public ActionResult<IEnumerable<User>> GetUsers()
		{
			return Ok(users);
		}

		[HttpGet("{id}")]
		public ActionResult<User> GetUserById(int id)
		{
			var user = users.FirstOrDefault(u => u.Id == id);
			if (user == null) return NotFound();
			return Ok(user);
		}

		[HttpPut("{id}")]
		public ActionResult UpdateUser(int id, User updatedUser)
		{
			var user = users.FirstOrDefault(u => u.Id == id);
			if (user == null) return NotFound();

			user.Name = updatedUser.Name;
			user.Email = updatedUser.Email;
			user.Password = updatedUser.Password;
			user.PhoneNumber = updatedUser.PhoneNumber;

			return Ok(user);
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteUser(int id)
		{
			var user = users.FirstOrDefault(u => u.Id == id);
			if (user == null) return NotFound();

			users.Remove(user);
			return NoContent();
		}
		[HttpPost]
		public ActionResult<User> CreateUser(User newUser)
		{
			newUser.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
			users.Add(newUser);

			// Return 201 Created with link to GET /api/users/{id}
			return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
		}
	}
}

