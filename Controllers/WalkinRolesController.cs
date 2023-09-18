using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace api_backend.Controllers;


[Route("[controller]")]
public class WalkinRolesController : ControllerBase
{
    readonly IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    


    [HttpGet("GetRolesUsingWalkinId")]
    public IActionResult Get(int id)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);

        DataTable walkinRolesTable = new();
        string query = $"SELECT * FROM roles WHERE roles_id IN (SELECT roles_id FROM walkin_db.walkin_roles WHERE walkin_id = {id});";
        connection.Open();

        using (MySqlCommand command = new(query, connection))
        {
            using MySqlDataAdapter adapter = new(command);
            adapter.Fill(walkinRolesTable);
        }

        return ToJson(walkinRolesTable);
    }

    


    private IActionResult ToJson(DataTable dataTable) {
        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        return Content(json, "application/json");
    }

}
