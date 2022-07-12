using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB.CUKCUK.API.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB.CUKCUK.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDepartmentName()
        {
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=Handanba1.;";
                var sqlConnection = new MySqlConnection(connectionString);

                string selectDepartment = "SELECT * FROM department";

                var listDepartment = sqlConnection.Query<Department>(selectDepartment);

                if (listDepartment != null)
                {
                    return StatusCode(StatusCodes.Status200OK, listDepartment);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
            
        }
    }
}
