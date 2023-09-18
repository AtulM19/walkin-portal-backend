using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace api_backend.Controllers;


[Route("[controller]")]
public class WalkinController : ControllerBase
{
    readonly IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    


    [HttpGet("GetWalkinData")]
    public IActionResult Get()
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);

        DataTable walkinTable = new();
        string walkinQuery = @"SELECT * FROM walkin_interview;";
        connection.Open();

        using (MySqlCommand command = new(walkinQuery, connection))
        {
            using MySqlDataAdapter adapter = new(command);
            adapter.Fill(walkinTable);
        }

        return ToJson(walkinTable);
    }

    [HttpGet("GetWalkinDataUsingId")]
    public IActionResult Get(int id)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);

        DataTable walkinTable = new();
        string walkinQuery = $"SELECT * FROM walkin_interview WHERE walkin_id = {id}";
        connection.Open();

        using (MySqlCommand command = new(walkinQuery, connection))
        {
            using MySqlDataAdapter adapter = new(command);
            adapter.Fill(walkinTable);
        }

        return ToJson(walkinTable);
    }

    


    private IActionResult ToJson(DataTable dataTable) {
        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        return Content(json, "application/json");
    }

}
