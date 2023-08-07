using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId,DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            try
            {
                string query = @"insert into dbo.Department values('" + dep.DepartmentName + @"') ";
                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Added Successfully");

               
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
            
        }


      
        [HttpPut]
        public IActionResult Put(Department dep)
        {
            try
            {
                string query = @"
            UPDATE dbo.Department
            SET DepartmentName = @DepartmentName
            WHERE DepartmentId = @DepartmentId";

                string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                        myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);

                        int rowsAffected = myCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok("Update Successfully");
                        }
                        else
                        {
                            return BadRequest("Update Failed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                            delete from dbo.Department 
                            where DepartmentId = " + id + @"
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Delete Successfully");
        }

    }
   
}
