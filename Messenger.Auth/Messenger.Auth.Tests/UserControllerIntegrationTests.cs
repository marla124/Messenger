using Messenger.Auth.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Messenger.Auth.IntegrationTests;

public class UserControllerIntegrationTests : BaseIntegrationTest
{
    private const string BaseUrl = "/api/User";

    [Fact]
    public async Task Create_ReturnSuccess()
    {
        var model = new RegisterModel
        {
            Email = "testemail3@gmail.com",
            Password = "password",
            PasswordConfirmation = "password"
        };
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var uri = $"{BaseUrl}/CreateUser";
        var response = await _httpClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var returnedUser = JsonConvert.DeserializeObject<UserViewModel>(await response.Content.ReadAsStringAsync());

        Assert.Equal(model.Email, returnedUser!.Email);
    }
}