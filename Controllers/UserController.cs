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
public class UserController : ControllerBase
{
    readonly IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
    


    [HttpGet("GetAllUsers")]
    public IActionResult Get()
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new MySqlConnection(connectionString);

        DataTable dataTable = new();
        string query = @"SELECT * FROM users";
        connection.Open();

        using (MySqlCommand command = new(query, connection))
        {
            using MySqlDataAdapter adapter = new(command);
            adapter.Fill(dataTable);
        }

        return ToJson(dataTable);

    }

    [HttpGet("GetUserById")]
    public IActionResult GetId(int id)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        using MySqlConnection connection = new MySqlConnection(connectionString);

        DataTable dataTable = new();
        string query = $"SELECT * FROM users where users.user_id = {id}";
        connection.Open();

        using (MySqlCommand command = new(query, connection))
        {
            using MySqlDataAdapter adapter = new(command);
            adapter.Fill(dataTable);
        }

        return ToJson(dataTable);
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] User userData)
    {
        if (userData == null)
        {
            Console.WriteLine("Received JSON data is null :  " + userData);
            return BadRequest("User data is required.");
        }

        try
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using MySqlConnection connection = new(connectionString);
            connection.Open();

            string receivedJson = userData.ToString();

            DateTime noticePeriodEndDate = new(userData.NoticePeriodEndDate.Year, userData.NoticePeriodEndDate.Month, userData.NoticePeriodEndDate.Day);



            string query = @"
                INSERT INTO users (
                    firstname, lastname, email, phone_number, portfolio_url,
                    job_roles_selected, referral, job_related_updates, percentage,
                    year_of_passing, qualification, stream, college, college_others,
                    college_location, applicant_type, years_of_experience, current_ctc,
                    expected_ctc, notice_period, notice_period_end_date, notice_period_duration,
                    appeared_for_zeus, appeared_for_role_in_zeus, resume, user_expertise_in,
                    user_familiar_in, user_other_expertise_in, user_other_familiar_in, photo, date_created, date_modified
                )
                VALUES (
                    @firstname, @lastname, @email, @phone_number, @portfolio_url,
                    @job_roles_selected, @referral, @job_related_updates, @percentage,
                    @year_of_passing, @qualification, @stream, @college, @college_others,
                    @college_location, @applicant_type, @years_of_experience, @current_ctc,
                    @expected_ctc, @notice_period, @notice_period_end_date, @notice_period_duration,
                    @appeared_for_zeus, @appeared_for_role_in_zeus, @resume, @user_expertise_in,
                    @user_familiar_in, @user_other_expertise_in, @user_other_familiar_in, @photo, DEFAULT, DEFAULT
                );
            ";

            using MySqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@firstname", userData.Firstname);
            command.Parameters.AddWithValue("@lastname", userData.Lastname);
            command.Parameters.AddWithValue("@email", userData.Email);
            command.Parameters.AddWithValue("@phone_number", userData.PhoneNumber);
            command.Parameters.AddWithValue("@portfolio_url", userData.PortfolioUrl);
            command.Parameters.AddWithValue("@job_roles_selected", userData.JobRolesSelected);
            command.Parameters.AddWithValue("@referral", userData.Referral);
            command.Parameters.AddWithValue("@job_related_updates", userData.JobRelatedUpdates);
            command.Parameters.AddWithValue("@percentage", userData.Percentage);
            command.Parameters.AddWithValue("@year_of_passing", userData.YearOfPassing);
            command.Parameters.AddWithValue("@qualification", userData.Qualification);
            command.Parameters.AddWithValue("@stream", userData.Stream);
            command.Parameters.AddWithValue("@college", userData.College);
            command.Parameters.AddWithValue("@college_others", userData.CollegeOthers);
            command.Parameters.AddWithValue("@college_location", userData.CollegeLocation);
            command.Parameters.AddWithValue("@applicant_type", userData.ApplicantType);
            command.Parameters.AddWithValue("@years_of_experience", userData.YearsOfExperience);
            command.Parameters.AddWithValue("@current_ctc", userData.CurrentCtc);
            command.Parameters.AddWithValue("@expected_ctc", userData.ExpectedCtc);
            command.Parameters.AddWithValue("@notice_period", userData.NoticePeriod);
            command.Parameters.AddWithValue("@notice_period_end_date", noticePeriodEndDate.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@notice_period_duration", userData.NoticePeriodDuration);
            command.Parameters.AddWithValue("@appeared_for_zeus", userData.AppearedForZeus);
            command.Parameters.AddWithValue("@appeared_for_role_in_zeus", userData.AppearedForRoleInZeus);
            command.Parameters.AddWithValue("@resume", userData.Resume);
            command.Parameters.AddWithValue("@user_expertise_in", userData.UserExpertiseIn);
            command.Parameters.AddWithValue("@user_familiar_in", userData.UserFamiliarIn);
            command.Parameters.AddWithValue("@user_other_expertise_in", userData.UserOtherExpertiseIn);
            command.Parameters.AddWithValue("@user_other_familiar_in", userData.UserOtherFamiliarIn);
            command.Parameters.AddWithValue("@photo", userData.Photo);

            command.ExecuteNonQuery();

            return Ok("User created successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }

    private IActionResult ToJson(DataTable dataTable) {
        string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
        return Content(json, "application/json");
    }
    static void PrintUserProperties(User user)
        {
            Type userType = typeof(User);
            PropertyInfo[] properties = userType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(user);
                Console.WriteLine($"{property.Name}: {value}");
            }
        }


}
