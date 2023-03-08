using C__Assignment.Model;
using C__Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace C__Assignment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private static NoRndUser? NoRndUser;
        public JsonFileRandomuserService UserService { get; }

        public UserController(JsonFileRandomuserService userService)
        {
            UserService = userService;
            NoRndUser = new NoRndUser();
        }
        [HttpGet]
        public string GetUsersData()//Couldn't 
        {
            for (int i = 0;i < 10; i++)
            {

            }
            string user = UserService.GetUser().ToString();
            return user;
        }

        //[HttpGet]
        //public string[] GetMostPopularCountry()
        //{

        //}
        //[HttpGet]
        //public string[] GetListOfMails()
        //{

        //}
        //[HttpGet]
        //public string[] GetTheOldestUser()
        //{

        //}

        //Part 2 
        //Please note the service class in the second excercise is not that relevant but in big projects no logic should pass
        //in the controllers!!

        [HttpPost]
        public ActionResult CreateNewUser([FromBody] NoRndUser request)
        {
            if (request?.Id == null)
                return BadRequest();

            NoRndUser = UserService.CreateUser(request);

            return Ok("The User has been created :) ");
        }
        [HttpGet]
        public NoRndUser GetNewUser([FromQuery] string id)
        {
            if (id == null || NoRndUser == null || id != NoRndUser.Id)
                return null;

            return NoRndUser;
        }
        [HttpPatch]
        public ActionResult UpdateUser([FromBody] NoRndUser request) 
        {
            if (request?.Id == null)
                return BadRequest();

            NoRndUser = UserService.UpdateUser(request);

            return Ok("The User has been updated :) ");
        }
    }
}
