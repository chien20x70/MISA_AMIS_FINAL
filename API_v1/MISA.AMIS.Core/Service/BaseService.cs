﻿using MISA.AMIS.Core.CustomAttribute;
using MISA.AMIS.Core.CustomExceptions;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Service
{
    #region BaseService
    public class BaseService<MISAEntity> : IBaseService<MISAEntity> where MISAEntity : class
    {

        #region PROPERTIES
        IBaseRepository<MISAEntity> _baseRepository;
        protected ServiceResult _serviceResult;
        #endregion

        #region CONSTRUCTOR
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Xóa 1 đối tượng.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult Delete(Guid entityId)
        {
            var rowAffects = _baseRepository.Delete(entityId);
            if (rowAffects > 0)
            {
                //_serviceResult.Data = Properties.Resources.Msg_Delete_Success;
                OnStateServiceResult(Properties.Resources.Msg_Delete_Success, StatusCode.Success, MISACode.Success);
            }
            else
            {
                //_serviceResult.Data = Properties.Resources.Msg_Delete_Fail;
                OnStateServiceResult(Properties.Resources.Msg_Delete_Fail, StatusCode.Error, MISACode.NoContent);
            }
            return _serviceResult;
        }

        /// <summary>
        /// Lấy danh sách tất cả các đối tượng.
        /// </summary>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult GetAll()
        {
            //TODO: Nếu không có bản ghi trong DB thì vẫn được coi là lấy thành công
            var entities = _baseRepository.GetAll();
            //_serviceResult.Data = entities;
            OnStateServiceResult(entities, StatusCode.Success, MISACode.Success);

            return _serviceResult;
        }

        /// <summary>
        /// Lấy 1 đối tượng theo ID.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>1 đối tượng có mã ID là entityId</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult GetById(Guid entityId)
        {

            var entity = _baseRepository.GetById(entityId);
            OnStateServiceResult(entity, StatusCode.Success, MISACode.Success);

            return _serviceResult;
        }

        /// <summary>
        /// Thêm mới 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult Insert(MISAEntity entity)
        {
            var status = Validate(entity, HTTPType.ADD);
            if (status == null)
            {
                var rowAffects = _baseRepository.Insert(entity);
                if (rowAffects > 0)
                {
                    OnStateServiceResult(Properties.Resources.Msg_Insert_Success, StatusCode.Success, MISACode.Success);
                }
                else
                {
                    OnStateServiceResult(Properties.Resources.Msg_Insert_Fail, StatusCode.Error, MISACode.NoContent);
                }
                return _serviceResult;
            }
            return status;
        }

        /// <summary>
        /// Sửa 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult Update(MISAEntity entity)
        {
            // Lấy ra ServiceResult khi validate
            var status = Validate(entity, HTTPType.UPDATE);
            // Kiểm tra == null thì Update còn ko thì trả về status dạng ServiceResult.
            if (status == null)
            {
                var rowAffects = _baseRepository.Update(entity);
                if (rowAffects > 0)
                {
                    OnStateServiceResult(Properties.Resources.Msg_Update_Success, StatusCode.Success, MISACode.Success);
                }
                else
                {
                    OnStateServiceResult(Properties.Resources.Msg_Update_Fail, StatusCode.Error, MISACode.NoContent);
                }
                return _serviceResult;
            }
            return status;
        }

        /// <summary>
        /// Validate chung dữ liệu
        /// </summary>
        /// <param name="entity">đối tượng truyền vào</param>
        /// <param name="http">Phương thức PUT hay POST</param>
        /// Created By: NXCHIEN 17/05/2021
        private ServiceResult Validate(MISAEntity entity, HTTPType http)
        {

            // Lấy ra tất cả property của đối tượng
            var properties = typeof(MISAEntity).GetProperties();
            var eName = typeof(MISAEntity).Name;
            foreach (var property in properties)
            {

                //Loại tất cả dấu cách của 1 object --- Tránh trường hợp khi thêm mã có dấu cách mà trùng với mà hiện tại vẫn thêm được.
                if (property.Name.Equals($"{eName}Code"))
                {
                    string propertyValue = (string)property.GetValue(entity);
                    property.SetValue(entity, propertyValue.Trim());
                }

                #region Attribute không được phép để trống
                // Lấy ra attribute của đối tượng
                var requiredAttribute = property.GetCustomAttributes(typeof(MISARequired), true);
                if (requiredAttribute.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Kiểm tra nếu giá trị null thì gán thành empty.
                    if (propertyValue == null || string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        // Lấy ra message lỗi của attribute.
                        var msgError = (requiredAttribute[0] as MISARequired).MsgErrorEmpty;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            msgError = property.Name + Properties.Resources.Required_NotEmpty_Message;
                        }
                        //throw new EmployeeExceptions(msgError);
                        return new ServiceResult()
                        {
                            Status = StatusCode.Error,
                            Code = MISACode.BadRequest,
                            Data = msgError,
                        };
                    }
                }
                #endregion

                //NOTREQUIRED: Đề bài không có yêu cầu check độ dài
                #region Attribute Check độ dài khi nhập
                // Kiểm tra độ dài mã Code của property
                var maxLengthAttribute = property.GetCustomAttributes(typeof(MISAMaxLength), true);
                if (maxLengthAttribute.Length > 0)
                {
                    // Lấy ra giá trị của property
                    var propertyValue = property.GetValue(entity);
                    // Lấy ra giá trị truyền vào của MISAMaxLength
                    var maxLength = (maxLengthAttribute[0] as MISAMaxLength).MaxLength;
                    // Kiểm tra độ dài
                    if (propertyValue.ToString().Length > maxLength)
                    {
                        var msgError = (maxLengthAttribute[0] as MISAMaxLength).MsgError_MaxLength;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            msgError = $"Độ dài của {property.Name} phải nhỏ hơn {maxLength}";
                        }
                        return new ServiceResult()
                        {
                            Status = StatusCode.Error,
                            Code = MISACode.BadRequest,
                            Data = msgError,
                        };
                    }
                }
                #endregion

                //NOTREQUIRED: Đề bài không yêu cầu Check email
                #region Attribute check xem có đúng là Email không
                // Kiểm tra email
                var emailAttribute = property.GetCustomAttributes(typeof(MISAEmail), true);
                if (emailAttribute.Length > 0)
                {
                    // Lấy giá trị email
                    var emailValue = property.GetValue(entity);
                    // Khởi tạo regex và kiểm tra
                    Regex regex = new Regex(Properties.Resources.Regex_String);
                    if (!regex.IsMatch(emailValue.ToString()))
                    {

                        var msgErrorEmail = (emailAttribute[0] as MISAEmail).MsgErrorEmail;
                        if (string.IsNullOrEmpty(msgErrorEmail.ToString()))
                        {
                            msgErrorEmail = property.Name + Properties.Resources.Required_Error_Message;
                        }
                        return new ServiceResult()
                        {
                            Status = StatusCode.Error,
                            Code = MISACode.BadRequest,
                            Data = msgErrorEmail,
                        };
                    }
                }
                #endregion
            }

            // Lấy ra ServiceResult và kiểm tra xem có null không.
            var customServiceResult = CustomValidate(entity, http);
            if (customServiceResult != null)
            {
                return customServiceResult;
            }
            return null;
        }

        /// <summary>
        /// Validate dữ liệu riêng từng đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <param name="http">Phương thức POST hay PUT</param>
        /// Created By: NXCHIEN 17/05/2021
        protected virtual ServiceResult CustomValidate(MISAEntity entity, HTTPType http)
        {
            return null;
        }

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NXCHIEN (17/05/2021)
        public ServiceResult GetMISAEntities(int pageSize, int pageIndex, string filter)
        {
            // Nếu nhập pageIndex <= 0 hoặc pageSize <= 0 thì trả về lỗi ServiceResult.
            if (pageIndex <= 0 || pageSize <= 0)
            {
                return new ServiceResult()
                {
                    Status = StatusCode.Error,
                    Code = MISACode.BadRequest,
                    Data = Properties.Resources.Msg_Param_Error,
                };
            }

            // Lấy ra danh sách đối tượng
            var entities = _baseRepository.GetMISAEntities(pageSize, pageIndex, filter);
            // Kiểm tra đối tượng ko null thì trả về Success còn null thì Erorr (hoặc Success nhưng NOContent).
            if (entities.Data.Any() && entities.TotalRecord != null)
            {
                OnStateServiceResult(entities, StatusCode.Success, MISACode.Success);
            }
            else
            {
                OnStateServiceResult("", StatusCode.Error, MISACode.NoContent);
            }

            return _serviceResult;
        }
        #endregion

        public void OnStateServiceResult(object data, StatusCode status, MISACode code)
        {
            _serviceResult.Data = data;
            _serviceResult.Status = status;
            _serviceResult.Code = code;
        }
    }
    #endregion
}
