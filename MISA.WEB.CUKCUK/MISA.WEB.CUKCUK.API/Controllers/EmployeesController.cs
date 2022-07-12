using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB.CUKCUK.API.Entities;
using MISA.WEB.CUKCUK.API.Entities.DTO;
using MySqlConnector;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.WEB.CUKCUK.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        /// <summary>
        /// Thêm mới một nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>ID của nhân viên</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            // khởi tạo kết nối db mysql
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=Handanba1.;";

                var mySqlconnection = new MySqlConnection(connectionString);

                // chuẩn bị câu lệnh INSERT INTO
                string insertCommand = "INSERT INTO employee (EmployeeID, EmployeeCode, EmployeeName, DateOfBirth, Gender, IdentityNumber, IdentityIssuedPlace, IdentityIssuedDate, Email, PhoneNumber, PositionID, DepartmentID, TaxCode, Salary, JoiningDate, WorkStatus, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy) VALUES(@EmployeeID, @EmployeeCode, @EmployeeName, @DateOfBirth, @Gender, @IdentityNumber, @IdentityIssuedPlace, @IdentityIssuedDate, @Email, @PhoneNumber, @PositionID, @DepartmentID, @TaxCode, @Salary, @JoiningDate, @WorkStatus, @CreatedDate, @CreatedBy, @ModifiedDate, @ModifiedBy); ";
                
                var employeeID = Guid.NewGuid();

                // Chuẩn bị tham số đầu vào cho câu lệnh INSERT INTO
                var parameters = new DynamicParameters();

                parameters.Add("@EmployeeID", employeeID);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@IdentityIssuedPlace", employee.IdentityIssuedPlace);
                parameters.Add("@IdentityIssuedDate", employee.IdentityIssuedDate);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@PositionID", employee.PositionID);
                parameters.Add("@DepartmentID", employee.DepartmentID);
                parameters.Add("@TaxCode", employee.TaxCode);
                parameters.Add("@Salary", employee.Salary);
                parameters.Add("@JoiningDate", employee.JoiningDate);
                parameters.Add("@WorkStatus", employee.WorkStatus);
                parameters.Add("@CreatedDate", employee.CreatedDate);
                parameters.Add("@CreatedBy", employee.CreatedBy);
                parameters.Add("@ModifiedDate", employee.ModifiedDate);
                parameters.Add("@ModifiedBy", employee.ModifiedBy);


                // THực hiện gọi vào db
                var affectedRows = mySqlconnection.Execute(insertCommand, parameters);

                // Xử lý kết quả từ DB
                if (affectedRows > 0)
                {
                    // Trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status201Created, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
             
            }
            catch (MySqlException mySqlException)
            {
                // TODO: Kiểm tra trùng mã nhân viên
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e003");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// Sửa một nhân viên theo ID nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employee"></param>
        /// <returns>trả về id nhân viên vừa sửa</returns>
        [HttpPut("{employeeID}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            
            try
            {// khởi tạo kết nối db mysql
                string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=Handanba1.;";
                var mySqlconnection = new MySqlConnection(connectionString);

                // chuẩn bị câu lệnh UPDATE
                string updateCommand = "UPDATE employee " +
                    "SET EmployeeCode = @EmployeeCode, " +
                    "EmployeeName = @EmployeeName, " +
                    "DateOfBirth = @DateOfBirth, " +
                    "Gender = @Gender, " +
                    "IdentityNumber = @IdentityNumber, " +
                    "IdentityIssuedPlace = @IdentityIssuedPlace, " +
                    "IdentityIssuedDate = @IdentityIssuedDate, " +
                    "Email = @Email, " +
                    "PhoneNumber = @PhoneNumber, " +
                    "PositionID = @PositionID, " +
                    "DepartmentID = @DepartmentID, " +
                    "TaxCode = @TaxCode, " +
                    "Salary = @Salary, " +
                    "JoiningDate = @JoiningDate, " +
                    "WorkStatus = @WorkStatus, " +
                    "ModifiedDate = @ModifiedDate, " +
                    "ModifiedBy = @ModifiedBy " +
                    "WHERE EmployeeID = @EmployeeID;";

                // Chuẩn bị tham số đầu vào cho câu lệnh INSERT INTO

                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@IdentityIssuedPlace", employee.IdentityIssuedPlace);
                parameters.Add("@IdentityIssuedDate", employee.IdentityIssuedDate);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@PositionID", employee.PositionID);
                parameters.Add("@DepartmentID", employee.DepartmentID);
                parameters.Add("@TaxCode", employee.TaxCode);
                parameters.Add("@Salary", employee.Salary);
                parameters.Add("@JoiningDate", employee.JoiningDate);
                parameters.Add("@WorkStatus", employee.WorkStatus);
                parameters.Add("@CreatedDate", employee.CreatedDate);
                parameters.Add("@CreatedBy", employee.CreatedBy);
                parameters.Add("@ModifiedDate", employee.ModifiedDate);
                parameters.Add("@ModifiedBy", employee.ModifiedBy);


                // Thực hiện gọi vào DB để chạy câu lệnh UPDATE với tham số đầu vào ở trên
                var numberOfAffectedRows = mySqlconnection.Execute(updateCommand, parameters);
                // Xử lý kết quả từ DB
                if (numberOfAffectedRows > 0)
                {
                    // Trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status201Created, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
               
                // Bổ sung try catch bắt exception
            }
            catch (MySqlException mySqlException)
            {
                // TODO: Kiểm tra trùng mã nhân viên
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e003");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
            catch (Exception exception)
            {
                // TODO: 
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        [HttpDelete("{employeeCode}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEmployee([FromRoute] string employeeCode)
        {
            try
            {
                //Kết nối DB
                string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=Handanba1.;";
                var mySqlconnection = new MySqlConnection(connectionString);

                //Lệnh delete theo ID nhân viên
                string deleteCommand = "DELETE FROM employee WHERE EmployeeCode = @EmployeeCode";

                // Chuẩn bị tham số đầu vào cho câu lệnh DELETE
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employeeCode);

                // Thực hiện gọi vào DB để chạy câu lệnh DELETE với tham số đầu vào ở trên
                int numberOfAffectedRows = mySqlconnection.Execute(deleteCommand, parameters);

                // Xử lý kết quả từ DB
                if (numberOfAffectedRows > 0)
                {
                    // Trả về dữ liệu cho client
                    return StatusCode(StatusCodes.Status201Created, employeeCode);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e002");
                }
            }
            catch (MySqlException mySqlException)
            {
                // TODO: 
                if (mySqlException.ErrorCode == MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "e003");
                }

                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
            catch (Exception exception)
            {
                
                return StatusCode(StatusCodes.Status400BadRequest, "e001");
            }
        }

        //[HttpGet("a")]
        //public IActionResult FilterEmployees([FromQuery] string? code, [FromQuery] string? name, [FromQuery] string? phoneNumber,
        //    [FromQuery] Guid? positionID, [FromQuery] Guid? departmentID, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        //{
        //    try
        //    {
        //        // Khởi tạo kết nối tới DB MySQL
        //        string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=12345678@Abc;";
        //        var mySqlConnection = new MySqlConnection(connectionString);

        //        // Chuẩn bị tên Stored procedure
        //        string storedProcedureName = "Proc_Employee_GetPaging";

        //        // Chuẩn bị tham số đầu vào cho stored procedure
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@$Skip", (pageNumber - 1) * pageSize);
        //        parameters.Add("@$Take", pageSize);
        //        parameters.Add("@$Sort", "ModifiedDate DESC");

        //        var whereConditions = new List<string>();
        //        if (code != null)
        //        {
        //            whereConditions.Add($"EmployeeCode LIKE '%{code}%'");
        //        }
        //        if (name != null)
        //        {
        //            whereConditions.Add($"EmployeeName LIKE '%{name}%'");
        //        }
        //        if (phoneNumber != null)
        //        {
        //            whereConditions.Add($"PhoneNumber LIKE '%{phoneNumber}%'");
        //        }
        //        if (positionID != null)
        //        {
        //            whereConditions.Add($"PositionID LIKE '%{positionID}%'");
        //        }
        //        if (departmentID != null)
        //        {
        //            whereConditions.Add($"DepartmentID LIKE '%{departmentID}%'");
        //        }
        //        string whereClause = string.Join(" AND ", whereConditions);
        //        parameters.Add("@$Where", whereClause);

        //        // Thực hiện gọi vào DB để chạy stored procedure với tham số đầu vào ở trên
        //        var multipleResults = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

        //        // Xử lý kết quả trả về từ DB
        //        if (multipleResults != null)
        //        {
        //            var employees = multipleResults.Read<Employee>();
        //            var totalCount = multipleResults.Read<long>().Single();
        //            return StatusCode(StatusCodes.Status200OK, new PagingData<Employee>()
        //            {
        //                Data = employees.ToList(),
        //                TotalCount = totalCount
        //            });
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest, "e002");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, "e001");
        //    }
        //}


        /// <summary>
        /// Hiển thị tất cả danh sách nhân viên
        /// </summary>
        /// <returns>Mảng bao gồm tất cả nhân viên</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<Employee>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult SelectAllEmployee()
        {
            try
            {
                // kết nối db
                string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=Handanba1.;";
                var mySqlconnection = new MySqlConnection(connectionString);

                // Lệnh SQL hiển thị tất cả nhân viên
                string selectEmployeesCommand = "SELECT * FROM employee;";

                //Thực hiên gọi vào DB trả ra 1 mảng
                var multipleResults = mySqlconnection.Query<Employee>(selectEmployeesCommand).ToList();

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


        /// <summary>
        /// API tìm kiếm nhân viên bằng mã NV
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns>Đối tượng muốn lấy thông tin chi tiết (Tìm kiếm gần đúng)</returns>
        [HttpGet("{employeeCode}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<Employee>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public IActionResult SelectOneEmployee([FromRoute] string employeeCode)
        {
            // kết nối db
            string connectionString = "Server=localhost;Port=3306;Database=test_webdev;Uid=root;Pwd=Handanba1.;";
            var mySqlconnection = new MySqlConnection(connectionString);
            
            // Lệnh SQL hiển thị theo mã nhân viên
            string findEmployeesCommand = $"SELECT * FROM employee WHERE EmployeeCode LIKE '%{employeeCode}%';";
            
            //Thực hiên gọi vào DB trả ra 1 mảng
            var multipleResults = mySqlconnection.Query<Employee>(findEmployeesCommand).ToList();

            if (multipleResults != null)
            {
                return StatusCode(StatusCodes.Status200OK, multipleResults);

            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, "e002");
            }


        }
    }
}