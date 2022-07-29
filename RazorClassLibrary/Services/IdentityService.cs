using Domain.Main;
using Domain.Main.DTO;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RazorClassLibrary.Models;
using System.Net.Http.Json;
using System.Text;

namespace RazorClassLibrary.Services
{
    public class IdentityService : IIdentityService
    {
        public string Token { get; set; } = default!;
        public UserDTO CurrentUser { get; set; } = default!;
        public string CurrentUserName
        {
            get { return (CurrentUser is not null)?$"{CurrentUser.FirstName} {CurrentUser.LastName}":"Unknown User"; }
        }

        private readonly HttpClient httpClient;

        private IAccessTokenProvider _tokenProvider = default!;

        public IdentityService(IConfiguration configuration, IAccessTokenProvider tokenProvider = default!)
        {
            _tokenProvider = tokenProvider;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(configuration["APIBaseURL"]);
            httpClient.Timeout = new TimeSpan(0, 0, 30);
        }

        public async Task<AuthenticateResponse> Authenticate(LoginRequest loginRequest)
        {
            string loginRequestStr = JsonConvert.SerializeObject(loginRequest);

            var response = await httpClient.PostAsync($"api/user/Authenticate",
                    new StringContent(loginRequestStr, Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var authenticateResponse = (await response.Content.ReadFromJsonAsync<AuthenticateResponse>())!;
                if (authenticateResponse is not null)
                {
                    CurrentUser = authenticateResponse.UserDetail;
                    Token = authenticateResponse.Token;
                    return authenticateResponse;
                }
            }

            CurrentUser = default!;
            Token = default!;
            return null!;
        }

        public async Task<bool> RegisterUser(RegisterUserDTO userDetails)
        {
            string registerUserRequestStr = JsonConvert.SerializeObject(userDetails);

            var response = await httpClient.PostAsync($"api/user/RegisterUser",
                    new StringContent(registerUserRequestStr, Encoding.UTF8, "application/json"));

            return (response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        public void SignOut()
        {
            CurrentUser = default!;
            Token = default!;
        }

        public async Task<string> GetToken()
        {
            if ((Token is null) && (_tokenProvider is not null))
            {
                var tokenResult = await _tokenProvider.RequestAccessToken();
                if (tokenResult.TryGetToken(out var token))
                {
                    Token = token.Value;
                }
            }
            return Token!;
        }

        public async Task<List<UserListResponse>> GetAllUsers()
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            var response = await httpClient.GetAsync($"api/user/GetAllUsers");

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
