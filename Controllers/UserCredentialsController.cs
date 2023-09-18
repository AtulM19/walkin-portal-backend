using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using api_backend.Models;
using System.Reflection;
using System.Globalization;

namespace api_backend.Controllers;


[Route("[controller]")]
public class UserCredentialsController : ControllerBase
{
    readonly IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    

    [HttpGet("GetUserByEmail")]
    public IActionResult GetUserByEmail(string email)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);

        DataTable dataTable = new();
        // string query = $"SELECT * FROM user_credentials where user_credentials.email = {email}";
        string query = $"SELECT * FROM user_credentials WHERE user_credentials.email = '{email}'";

        connection.Open();

        using (MySqlCommand command = new(query, connection))
        {
            using MySqlDataAdapter adapter = new(command);
            adapter.Fill(dataTable);
        }

        return ToJson(dataTable);
    }

    [HttpPost("CreateUserCredentials")]
    public async Task<IActionResult> CreateUserCredentials([FromBody] UserCredentials UserCredentialsData)
    {
        if (UserCredentialsData == null)
        {
            return BadRequest("User data is required.");
        }

        try
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using MySqlConnection connection = new(connectionString);
            connection.Open();

            string receivedJson = UserCredentialsData.ToString();

            string query = @"
                INSERT INTO user_credentials (
                    email, password, date_created, date_modified
                )
                VALUES (
                    @email, @password, DEFAULT, DEFAULT
                );
            ";

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@email", UserCredentialsData.Email);
            command.Parameters.AddWithValue("@password", UserCredentialsData.Password);

            command.ExecuteNonQuery();

            return Ok("User created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("fatttt gaya");
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    private IActionResult ToJson(DataTable dataTable) {
        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        return Content(json, "application/json");
    }
    static void PrintUserCredentialsProperties(UserCredentials user)
        {
            Type userType = typeof(UserCredentials);
            PropertyInfo[] properties = userType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(user);
                Console.WriteLine($"{property.Name}: {value}");
            }
        }


}
