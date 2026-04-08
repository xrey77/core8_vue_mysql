using Xunit;
using Microsoft.AspNetCore.Mvc.Testing; // Added this
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace core8_vue_mysql.Tests;

public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UnitTest1(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJSZXkiLCJuYmYiOjE3NzU2NDkwNTUsImV4cCI6MTc3NTY3Nzg1NSwiaWF0IjoxNzc1NjQ5MDU1LCJpc3MiOiJSZXluYWxkIEdyYWdhc2luIiwiYXVkIjoiVXNlcnMifQ.uZIqOAr99YPvdCK7YHgRYpb3WI8lNFmbCdTuyJ0JyaI";

        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
    }

   [Fact]  // 1
    public async Task GetById_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/api/getbyid/1");
        response.EnsureSuccessStatusCode();
    }

//     [Fact]  // 2
//     public async Task GetAll_ReturnsSuccess()
//     {
//         var response = await _client.GetAsync("/api/getall"); 
//         response.EnsureSuccessStatusCode();
//     }

//     [Fact]  // 3
//     public async Task UpdateProfile_ReturnsSuccess()
//     {
//         var updatedData = new 
//         { 
//             FirstName = "Reynaldos", 
//             Lastname = "Gragasin-Marquez",
//             Mobile = "+61-343434343"
//         };
//         var response = await _client.PatchAsJsonAsync("/api/updateprofile/1", updatedData);
        
//         response.EnsureSuccessStatusCode();
//     }
    
//     [Fact] // 4
//     public async Task UpdatePassword_ReturnsSuccesss()
//     {
//         var updatedData = new 
//         { 
//             Password = "rey", 
//         };
//         var response = await _client.PatchAsJsonAsync("/api/updatepassword/1", updatedData);        
//         response.EnsureSuccessStatusCode();

//     }

//     [Fact] // 5
//     public async Task ActivateMfa_ReturnsSuccess()
//     {
//         var updatedData = new 
//         { 
//             Twofactorenabled = false
//         };
//         var response = await _client.PatchAsJsonAsync("/api/activatemfa/1", updatedData);        
//         response.EnsureSuccessStatusCode();
//     }

    // [Fact] // 6
    // public async Task ResetPassword_ReturnsSuccess()
    // {
    //     var email = "rey@yahoo.com";
    //     var encodedEmail = Uri.EscapeDataString(email);        


    //     var updatedData = new 
    //     { 
    //         Mailtoken = 1111, // NOTE : This mailtoken should be present in the users table
    //         Password = "nald"
    //     };
    //     var response = await _client.PatchAsJsonAsync($"/api/resetpassword/{encodedEmail}", updatedData);        
    //     response.EnsureSuccessStatusCode();
    // }

    // [Fact] // 7
    // public async Task validateOTP_RetursSuccess()
    // {
    //     var postData = new 
    //     { 
    //         Id = 1,
    //         Otp = "123446"  // NOTE : Enter actual OTP code from Google Authenticator app
    //     };
    //     var response = await _client.PostAsJsonAsync($"/api/validateotp", postData);        
    //     response.EnsureSuccessStatusCode();
    // }

    // [Fact]
    // public async Task ActivateUser_ReturnsSuccess()
    // {
    //     var activationValue = new {
    //         Activation = 1
    //     };

    //     var response = await _client.PatchAsJsonAsync($"/api/activateuser/1", activationValue);
    //     response.EnsureSuccessStatusCode();
    // }

    // [Fact]
    // public async Task ListProducts_ReturnsSuccess()
    // {
    //     var response = await _client.GetAsync($"/api/listproducts/1");
    //     response.EnsureSuccessStatusCode();
    // }

    // [Fact]
    // public async Task SearchProducts_ReturnsSuccess()
    // {
    //     var response = await _client.GetAsync($"/api/searchproducts/1/cineo");
    //     response.EnsureSuccessStatusCode();
    // }

    // [Fact]
    // public async Task Signin_ReturnsSuccess()
    // {
    //     var postData = new {
    //         Username = "Rey",
    //         Password = "rey"
    //     };

    //     var response = await _client.PostAsJsonAsync($"/signin", postData );
    //     response.EnsureSuccessStatusCode();
    // }

    // [Fact]
    // public async Task Signup_ReturnsSuccess()
    // {
    //     var postData = new {
    //         Firstname = "Lilian",
    //         Lastname = "Hervias",
    //         Email = "lilian@hervias.com",
    //         Mobile = "24234234234",
    //         Username = "Lilian",
    //         Password = "rey"
    //     };
        
    //     var response = await _client.PostAsJsonAsync($"/signup", postData );
    //     response.EnsureSuccessStatusCode();
    // }
}
