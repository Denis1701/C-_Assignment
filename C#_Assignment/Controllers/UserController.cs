using C__Assignment.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

namespace C__Assignment.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private static List<NoRndUser>? noRndUsers = new List<NoRndUser>();//Second ex var
        private static int myId = 1;//Second ex var

        private readonly ILogger<UserController> _logger;//Logger var


        //Constructor
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("UsersData")]
        public async Task<IActionResult> GetUsersData([FromQuery] string gender)
        {
            try
            {
                if (gender != "male" && gender != "female")
                {
                    _logger.LogError("Invalid gender were inserted", gender);
                    return BadRequest("Invalid Gender - Please insert male or female");
                }
                using (var client = new HttpClient())
                {
                    var url = $"https://randomuser.me/api/?results=10&gender={gender}";
                    var response = await client.GetAsync(url);
                    var content = await response.Content.ReadAsStringAsync();
                    return Ok(content);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " - Oopsy an error occured");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("MostPopularCountry")]
        public async Task<IActionResult> GetMostPopularCountry()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://randomuser.me/api/?results=5000";
                    var response = await client.GetAsync(url);
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<User>(content)!.Results;
                    var countries = users!.Select(u => u.Location!.Country).ToList();
                    var mostPopularCountry = countries.GroupBy(c => c)
                                                      .OrderByDescending(c => c.Count())
                                                      .FirstOrDefault()!
                                                      .Key;
                    return Ok(mostPopularCountry);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " - Oopsy an error occured");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ListOfMails")]
        public async Task<IActionResult> GetListOfMails()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://randomuser.me/api/?results=30";
                    var response = await client.GetAsync(url);
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<User>(content)!.Results;
                    var mails = users!.Select(u => u.Email).ToList();
                    return Ok(mails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " - Oopsy an error occured");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("TheOldestUser")]
        public async Task<IActionResult> GetTheOldestUser()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"https://randomuser.me/api/?results=100";
                    var response = await client.GetAsync(url);
                    var content = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<User>(content)!.Results;
                    var oldestUser = users!.OrderBy(u => u.Dob!.Age).LastOrDefault();
                    return Ok($"{oldestUser!.Name!.First} {oldestUser.Name.Last} {oldestUser!.Dob!.Age}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " - Oopsy an error occured");
                return BadRequest(ex.Message);
            }
        }



        //Part 2 

        [HttpPost("NewUser")]
        public IActionResult CreateNewUser([FromBody] NoRndUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User Object Insert", user);
                    return BadRequest();
                }

                user.Id = myId++;
                noRndUsers!.Add(user);
                Console.WriteLine(noRndUsers.Count);
                return StatusCode(201, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " - Oopsy an error occured");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("NewUser")]
        public IActionResult GetNewUser()
        {
            try
            {
                var lastUser = noRndUsers!.LastOrDefault();

                if (lastUser == null)
                {
                    _logger.LogInformation("Empty users list Error occured");
                    return NotFound();
                }

                return Ok(lastUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " - Oopsy an error occured");
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch("UserData")]
        public ActionResult UpdateUserData([FromBody] NoRndUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid User Object Insert", user);
                    return BadRequest();
                }

                var lastUser = noRndUsers!.LastOrDefault();

                if (lastUser == null)
                {
                    _logger.LogInformation("Empty users list Error occured");
                    return BadRequest();
                }

                lastUser.Name = user.Name;
                lastUser.Email = user.Email;
                lastUser.Phone = user.Phone;
                lastUser.BirthDate = user.BirthDate;
                lastUser.Country = user.Country;

                return Ok(lastUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " - Oopsy an error occured");
                return BadRequest(ex.Message);
            }
        }

    }
}
