﻿using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Service
{
    public interface IEmployeeService: IBaseService<Employee>
    {
        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="employeeFilter">Bộ lọc nhân viên</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NXCHIEN (09/05/2021)
        public Paging<Employee> GetEmployees(int pageSize, int pageIndex, string filter);

        /// <summary>
        /// Lấy ra EmployeeCode lớn nhất trong DB
        /// </summary>
        /// <returns>EmployeeCode</returns>
        /// Created By: NXCHIEN 09/05/2021
        public string GetEmployeeCodeMax();

        /// <summary>
        /// Export file excel xuất khẩu toàn bộ nhân viên
        /// </summary>
        /// <param name="pageSize">số nhân viên / trang</param>
        /// <param name="pageIndex">Trang số bao nhiêu</param>
        /// <param name="filter">lọc bằng chuỗi string</param>
        /// <returns>Stream</returns>
        /// CreatedBy: NXCHIEN 11/05/2021
        public Stream ExportExcel();

        /// <summary>
        /// Lấy ra 1 nhân viên theo Id và gán mã code nhân viên là lớn nhất
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <returns>Nhân viên được gán mã code lớn nhất</returns>
        /// Created By: NXCHIEN 16/05/2021
        public Employee GetDuplicateEmployee(Guid id);
    }
}
