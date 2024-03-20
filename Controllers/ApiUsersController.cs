using EWM.Connecting;
using EWM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EWM.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        public readonly EVMDataContext _db;
        public ApiUsersController(EVMDataContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public IEnumerable<UsersModel> Get()
        {
            var users = _db.UsersViewModel.Select(s => new UsersModel
            {
                name = s.name,
                email = s.email,
            }).ToList();
            return users;
        }
        [HttpGet("{id}")]
        public UsersModel Get(int id)
        {
            var user = _db.UsersViewModel.Select(s => new UsersModel
            {
                name = s.name,
                email = s.email,
            }).Where(a => a.id == id).FirstOrDefault();
            return user;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsersModel _user)
        {
            var user = new UsersModel()
            {
                name = _user.name,
                email = _user.email,
            };
            _db.UsersViewModel.Add(user);
            await _db.SaveChangesAsync();
            if (user.id > 0)
            {
                return Ok(1);
            }
            return Ok(0);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsersModel _user)
        {
            var user = _db.UsersViewModel.Find(id);
            user.name = _user.name;
            user.email = _user.email;
            await _db.SaveChangesAsync();
            return Ok(1);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _db.UsersViewModel.Find(id);
            _db.UsersViewModel.Remove(user);
            await _db.SaveChangesAsync();
            return Ok(1);
        }
    }
}
