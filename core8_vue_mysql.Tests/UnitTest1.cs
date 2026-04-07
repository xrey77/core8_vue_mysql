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
        string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJSZXkiLCJuYmYiOjE3NzU1NTEyNTQsImV4cCI6MTc3NTU1NDg1NCwiaWF0IjoxNzc1NTUxMjU0LCJpc3MiOiJSZXluYWxkIEdyYWdhc2luIiwiYXVkIjoiVXNlcnMifQ.fB-zwdfOlmgccFYqpojM6qhvJn2y2_QROJpOJlQWEyc";

        _client.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
    }

   [Fact]  // 1
    public async Task GetById_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/api/getbyid/1");
        response.EnsureSuccessStatusCode();
    }

    [Fact]  // 2
    public async Task GetAll_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/api/getall"); 
        response.EnsureSuccessStatusCode();
    }

    [Fact]  // 3
    public async Task UpdateProfile_ReturnsSuccess()
    {
        var updatedData = new 
        { 
            FirstName = "Reynaldos", 
            Lastname = "Gragasin-Marquez",
            Mobile = "+61-343434343"
        };
        var response = await _client.PatchAsJsonAsync("/api/updateprofile/1", updatedData);
        
        response.EnsureSuccessStatusCode();
    }
    
    [Fact] // 4
    public async Task UpdatePassword_ReturnsSuccesss()
    {
        var updatedData = new 
        { 
            Password = "rey", 
        };
        var response = await _client.PatchAsJsonAsync("/api/updatepassword/1", updatedData);        
        response.EnsureSuccessStatusCode();

    }

    [Fact] // 5
    public async Task ActivateMfa_ReturnsSuccess()
    {
        var updatedData = new 
        { 
            Twofactorenabled = false
        };
        var response = await _client.PatchAsJsonAsync("/api/activatemfa/1", updatedData);        
        response.EnsureSuccessStatusCode();
    }

    [Fact] // 6
    public async Task ResetPassword_ReturnsSuccess()
    {
        var email = "rey@yahoo.com";
        var encodedEmail = Uri.EscapeDataString(email);        


        var updatedData = new 
        { 
            Mailtoken = 1111,
            Password = "nald"
        };
        var response = await _client.PatchAsJsonAsync($"/api/resetpassword/{encodedEmail}", updatedData);        
        response.EnsureSuccessStatusCode();

    }

}
