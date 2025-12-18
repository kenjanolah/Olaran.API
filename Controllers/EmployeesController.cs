using System.Data;
using Dapper.Web.Dto;
using Dapper.Web.models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Dapper.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        public EmployeesController()
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            using var connection = CreateConnection();

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "SELECT * FROM employees";

                var employees = await dbConnection.QueryAsync<Employee>(sql);

                return Ok(employees);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            using var connection = CreateConnection();

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "SELECT * FROM employees where id = @id";

                var employees = await dbConnection.QueryFirstOrDefaultAsync<Employee>(sql, new { id });

                return Ok(employees);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO employee)
        {
            using var connection = CreateConnection();

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = $@"INSERT INTO employees 
                            (fullname, address, age, birthday) 
                        VALUES 
                            (@fullname, @address, @age, @birthday)";

                try
                {
                    var result = await dbConnection.ExecuteAsync(sql, employee);
                    return Ok("Employee Record Addded Successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            using var connection = CreateConnection();

            using (IDbConnection dbConnection = CreateConnection())
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();

                var sql = "DELETE FROM employees where id = @id";

                var affectedRows = await dbConnection.ExecuteAsync(sql, new { id });

                if (affectedRows > 0)
                {
                    return Ok("Employee Record Deleted Successfully");
                }
                else
                {
                    return NotFound("Employee Record Not Found");
                }
            }
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection("Server=localhost;Database=olaranDb;User Id=root;Password=admin;");
        }
    }
}