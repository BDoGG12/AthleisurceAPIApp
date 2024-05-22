using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using AthleisurceAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AthleisurceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMongoCollection<CustomerModel> _customerCollection;
        public AuthController(IMongoDatabase database)
        {
            _customerCollection = database.GetCollection<CustomerModel>("Athleisurce_Customers");
        }

        [HttpPost("api/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _customerCollection.Find(u => u.EmailAddress == request.Email && u.Password == request.Password).FirstOrDefaultAsync();

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new { id = user.Id, email = user.EmailAddress });
        }
    }
}
