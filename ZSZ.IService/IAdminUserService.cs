using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface IAdminUserService:IServiceSupport
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="password">密码</param>
        /// <param name="email"></param>
        /// <param name="cityId">城市Id(null表示总部)</param>
        /// <returns></returns>
        long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId);

        /// <summary>
        /// 更改用户
        /// </summary>
        /// <param name="name">用户姓名</param>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="password">密码</param>
        /// <param name="email"></param>
        /// <param name="cityId">城市Id(null表示总部)</param>
        /// <returns></returns>
        void UpdateAdminUser(long id,string name, string phoneNum, string password, string email, long? cityId);

        /// <summary>
        /// 获取CityId这个城市下的管理员
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        AdminUserDTO[] GetAll(long? cityId);

        /// <summary>
        /// 获取所有管理员
        /// </summary>
        /// <returns></returns>
        AdminUserDTO[] GetAll();

        /// <summary>
        /// 根据Id获取DTO
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <returns></returns>
        AdminUserDTO GetById(long id);

        /// <summary>
        /// 根据手机号获取DTO
        /// </summary>
        /// <param name="phoneNum">手机号</param>
        /// <returns></returns>
        AdminUserDTO GetByPhoneNum(string phoneNum);

        /// <summary>
        /// 检查用户名密码是否正确
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckLogin(string phoneNum, string password);

        /// <summary>
        /// 删除标记
        /// </summary>
        /// <param name="adminUserId"></param>
        void MarkDeleted(long adminUserId);

        /// <summary>
        /// 判断adminUserId这个用户是否有permissionName这个权限项
        /// </summary>
        /// <param name="adminUserId">用户编号</param>
        /// <param name="permissionName">权限名称</param>
        /// <returns></returns>
        /// <example>HasPermission(3,"User.Add")</example>
        bool HasPermission(long adminUserId, string permissionName);

        /// <summary>
        /// 记录错误登录一次
        /// </summary>
        /// <param name="id"></param>
        void RecordLoginError(long id);
        
        /// <summary>
        /// 重置登录错误信息
        /// </summary>
        /// <param name="id"></param>
        void ResetLoginError(long id);
    }
}
