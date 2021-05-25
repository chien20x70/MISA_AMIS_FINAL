﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Enums
{
    public enum MISACode
    {
        /// <summary>
        /// thành công
        /// </summary>
        Success = 200,
        /// <summary>
        /// thêm mới bản ghi thành công
        /// </summary>
        Created = 201,
        /// <summary>
        /// không có bản ghi nào được truyền vào 
        /// </summary>
        NoContent = 204,
        /// <summary>
        /// sai cú pháp truyền vào 
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// không tìm thấy trang yêu cầu
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// lỗi phía server.
        /// </summary>
        ErrServer = 500,
    }
}
