using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    #region EmployeeController
    public class EmployeeController : BaseController<Employee>
    {
        #region PROPERTIES
        IEmployeeService _employeeService;
        IEmployeeRepository _employeeRepository;
        #endregion


        #region CONSTRUCTOR
        public EmployeeController(IEmployeeService employeeService, IEmployeeRepository employeeRepository) : base(employeeRepository, employeeService)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Lấy ra EmployeeCode lớn nhất trong DB
        /// </summary>
        /// <returns>EmployeeCode</returns>
        /// Created By: NXCHIEN 17/05/2021
        [HttpGet("employeeCode")]
        public IActionResult GetCode()
        {
            try
            {
                _serviceResult = _employeeService.GetEmployeeCodeMax();
            }
            catch (Exception ex)
            {
                OnServiceResultException(ex);
            }
            return Ok(_serviceResult);
        }

        /// <summary>
        /// Export file excel
        /// </summary>
        /// <returns>
        ///     - Thành công: 200
        ///     - Lỗi server: 500
        /// </returns>
        /// CreatedBy: NXCHIEN (17/05/2021)
        [HttpGet("ExportingExcel")]
        public IActionResult ExportingExcel()
        {
            var stream = _employeeService.ExportExcel();
            string excelName = $"Danh-sach-nhan-vien-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        /// <summary>
        /// Lấy ra 1 nhân viên theo Id và gán mã code nhân viên là lớn nhất
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <returns>Nhân viên được gán mã code lớn nhất</returns>
        /// Created By: NXCHIEN 17/05/2021
        [HttpGet("EmployeeCopy")]
        public IActionResult GetEmployeeCopy(Guid id)
        {
            try
            {
                _serviceResult = _employeeService.GetDuplicateEmployee(id);
            }
            catch (Exception ex)
            {
                OnServiceResultException(ex);
            }
            return Ok(_serviceResult);
        } 
        #endregion
    } 
    #endregion
}
