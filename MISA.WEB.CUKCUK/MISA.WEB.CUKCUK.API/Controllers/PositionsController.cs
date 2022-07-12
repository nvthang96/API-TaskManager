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
    public class PositionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPositionName()
        {
            try
            {
                // kết nối db
                string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=Handanba1.;";
                var mySqlconnection = new MySqlConnection(connectionString);

                // Lệnh SQL hiển thị tất cả nhân viên
                string selectPositionCommand = "SELECT * FROM positions";

                //Thực hiên gọi vào DB trả ra 1 mảng
                var multipleResults = mySqlconnection.Query<Positions>(selectPositionCommand).ToList();

                if (multipleResults != null)
                {
                    return StatusCode(StatusCodes.Status200OK, multipleResults);

                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (Exception ex) {
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }
    }
}
