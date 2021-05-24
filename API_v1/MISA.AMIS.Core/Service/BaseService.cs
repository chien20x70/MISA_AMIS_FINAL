using MISA.AMIS.Core.CustomAttribute;
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
        #endregion

        #region CONSTRUCTOR
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
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
            var res = new ServiceResult();
            var rowAffects = _baseRepository.Delete(entityId);
            if (rowAffects > 0)
            {
                res.Status = StatusCode.Success;
                //TODO: chỉ cần trả về Status khi thành công thôi đúng ko? có cần message ko?
                //res.Message = "Xóa thành công";
            }
            else
            {
                res.Status = StatusCode.Error;
                res.Code = "NODATA";
                res.Message = "Xóa thất bại!";
            }
            return res;
        }

        /// <summary>
        /// Lấy danh sách tất cả các đối tượng.
        /// </summary>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult GetAll()
        {
            //TODO: Nếu không có bản ghi trong DB thì vẫn được coi là lấy thành công
            var res = new ServiceResult();
            var entities = _baseRepository.GetAll();
            if (entities.Count() > 0)
            {
                res.Data = entities;
                res.Status = StatusCode.Success;
            }
            else
            {
                //TODO: không có bản ghi nào thì trả về lỗi
                res.Status = StatusCode.Error;
                res.Code = "NODATA";
                res.Message = "Không có nhân viên nào trong hệ thống!";
            }
            return res;
        }

        /// <summary>
        /// Lấy 1 đối tượng theo ID.
        /// </summary>
        /// <param name="entityId">Mã ID của đối tượng.</param>
        /// <returns>1 đối tượng có mã ID là entityId</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult GetById(Guid entityId)
        {
            var res = new ServiceResult();
            var entity = _baseRepository.GetById(entityId);
            if (entity != null)
            {
                res.Data = entity;
            }
            else
            {
                res.Status = StatusCode.Error;
                res.Code = "NODATA By ID";
                res.Message = $"Không tồn tại nhân viên có mã ID là {entityId}";
            }
            return res;
        }

        /// <summary>
        /// Thêm mới 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm mới.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public ServiceResult Insert(MISAEntity entity)
        {
            var status = Validate(entity, HTTPType.POST);
            if (status == null)
            {
                var res = new ServiceResult();
                var rowAffects = _baseRepository.Insert(entity);
                if (rowAffects > 0)
                {
                    res.Data = entity;
                    res.Status = StatusCode.Success;
                }
                else
                {
                    res.Status = StatusCode.Error;
                    res.Code = "CANT INSERT";
                    res.Message = "Thất bại";
                }
                return res; 
            }
            return status;
        }

        /// <summary>
        /// Sửa 1 đối tượng.
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa.</param>
        /// <returns>số dòng bị trong bảng trong DB bị ảnh hưởng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public int Update(MISAEntity entity)
        {
            Validate(entity, HTTPType.PUT);
            return _baseRepository.Update(entity);
        }

        /// <summary>
        /// Phân trang đối tượng.
        /// </summary>
        /// <param name="pageSize">số đối tượng trên 1 trang.</param>
        /// <param name="pageIndex">Trang số bao nhiêu.</param>
        /// <returns>Mảng danh sách đối tượng</returns>
        /// CreatedBy: NXChien (17/05/2021)
        public IEnumerable<MISAEntity> GetMISAEntities(int pageSize, int pageIndex)
        {
            return _baseRepository.GetMISAEntities(pageSize, pageIndex);
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
            foreach (var property in properties)
            {

                //Loại tất cả dấu cách của 1 object --- Tránh trường hợp khi thêm mã có dấu cách mà trùng với mà hiện tại vẫn thêm được.
                if (property.Name == typeof(MISAEntity).Name + "Code")
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
                        return new ServiceResult() { Status = StatusCode.Exception, Message = msgError};
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
                        return new ServiceResult() { Status = StatusCode.Exception, Message = msgError };
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
                        return new ServiceResult() { Status = StatusCode.Exception, Message = msgErrorEmail };
                    }
                }
                #endregion
            }
            CustomValidate(entity, http);
            return null;
        }

        /// <summary>
        /// Validate dữ liệu riêng từng đối tượng
        /// </summary>
        /// <param name="entity">đối tượng cần validate</param>
        /// <param name="http">Phương thức POST hay PUT</param>
        /// Created By: NXCHIEN 17/05/2021
        protected virtual void CustomValidate(MISAEntity entity, HTTPType http) { }
        #endregion

    }
    #endregion
}
