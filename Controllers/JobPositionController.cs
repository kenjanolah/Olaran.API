using System.Data;
using Dapper.Web.Dto;
using Dapper.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Dapper.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobPositionController : ControllerBase
    {
        public JobPositionController()
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetJobPositions()
        {
            using var connection = CreateConnection();

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "SELECT * FROM job_positions";

                var JobPositions = await dbConnection.QueryAsync<JobPositions>(sql);

                return Ok(JobPositions);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateJobPosition(CreateJobDTO jobPosition)
        {
            using var connection = CreateConnection();

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = $@"INSERT INTO job_positions (position_name, beginning_salary) VALUES (@position_name, @beginning_salary)";

                try
                {
                    var result = await dbConnection.ExecuteAsync(sql, jobPosition);
                    return Ok("Job Position Registered Successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(int id)
        {
            using var connection = CreateConnection();

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "DELETE FROM job_positions where id = @id";

                try
                {
                    var result = await dbConnection.ExecuteAsync(sql, new { id });
                    return Ok("Job Position Deleted Successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosition(int id, UpdateJobDTO jobPosition)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                jobPosition.id = id;

                var sql = $@"UPDATE job_positions 
                    SET position_name = @position_name, beginning_salary = @beginning_salary 
                    WHERE id = @id";

                try
                {
                    var result = await dbConnection.ExecuteAsync(sql, jobPosition);

                    return Ok("Job Position Updated Successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPositionById(int id)
        {
            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "SELECT * FROM job_positions WHERE id = @id";

                try
                {
                    var jobPosition = await dbConnection.QuerySingleOrDefaultAsync<JobPositions>(sql, new { id });

                    if (jobPosition == null)
                    {
                        return NotFound("Job Position Not Found");
                    }

                    return Ok(jobPosition);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        private IDbConnection CreateConnection()
        {
            return new MySqlConnection("Server=localhost;Database=olaranDb;User Id=root;Password=admin;");
        }
    }
}