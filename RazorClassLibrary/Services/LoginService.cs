using Domain.Main;
using Domain.Main.DTO;
using Newtonsoft.Json;
using RazorClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary.Services
{
    public class LoginService : ILoginService
    {
        public static UserDTO UserDetails = new();
        public static string Token = default!;

        public async Task<AuthenticateResponse> Authenticate(LoginRequest loginRequest)
        {
            using (var client = new HttpClient())
            {
                string loginRequestStr = JsonConvert.SerializeObject(loginRequest);

                var response = await client.PostAsync("https://localhost:7250/User/Authenticate",
                      new StringContent(loginRequestStr, Encoding.UTF8,
                      "application/json"));

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return (await response.Content.ReadFromJsonAsync<AuthenticateResponse>())!;
                }
                else
                {
                    return null!;
                }
            }
        }

        public async Task<List<UserListResponse>> GetAllUsers()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                var response = await client.GetAsync("http://localhost:7250/User/GetAllUsers");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<UserListResponse>>(json)!;
                }
                else
                {
                    return null!;
                }
            }
        }
    }
}
