using C__Assignment.Model;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace C__Assignment.Services
{
    public class JsonFileRandomuserService
    {
        public JsonFileRandomuserService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        public string JsonFileURL { get; private set; } = string.Format("https://randomuser.me/api/");
        

        public User GetUser()
        {
            
            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("https://randomuser.me/api/");
            }
            
            return JsonConvert.DeserializeObject<User>(json);
        }


        //Ex2
        public NoRndUser CreateUser(NoRndUser noRndUser)
        {
            return new NoRndUser
            {
                Id = noRndUser.Id,
                Name = noRndUser.Name,
                Email = noRndUser.Email,
                BirthDate = noRndUser.BirthDate,
                Phone = noRndUser.Phone,
                Country = noRndUser.Country
            };
        }
        public NoRndUser UpdateUser(NoRndUser noRndUser)
        {
            return new NoRndUser
            {
                Id = noRndUser.Id,
                Name = noRndUser.Name,
                Email = noRndUser.Email,
                BirthDate = noRndUser.BirthDate,
                Phone = noRndUser.Phone,
                Country = noRndUser.Country
            };
        }
    }
}
