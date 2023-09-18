using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace api_backend.Controllers;


[Route("[controller]")]
public class TimeSlotAvailableController : ControllerBase
{
    readonly IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    


    [HttpGet("GetTimeSlotUsingWalkinId")]
    public IActionResult Get(int id)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new(connectionString);

        DataTable timeSlotTable = new();
        string query = $"SELECT timeslot_id, start_time, end_time FROM time_slots_available WHERE walkin_id = {id};";
        connection.Open();

        using (MySqlCommand command = new(query, connection))
        {
            using MySqlDataAdapter adapter = new(command);
            adapter.Fill(timeSlotTable);
        }

        return ToJson(timeSlotTable);
    }

    


    private IActionResult ToJson(DataTable dataTable) {
        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        return Content(json, "application/json");
    }

}
