using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.COMMON;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    public class AminUserService : IAdminUserService
    {
        public long AddAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            AdminUserEntity user = new AdminUserEntity();
            user.CityId = cityId;
            user.Email = email;
            user.Name = name;
            user.PhoneNum = phoneNum;
            string salt = CommonHelper.CreateVerifyCode(5);//盐
            user.PasswordSalt = salt;
            //MD5(盐+密码)
            string pwdHash = CommonHelper.CalcMD5(salt + password);
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                bool exists = bs.GetAll().Any(x => x.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("手机号已经存在:" + phoneNum);
                }
                ctx.AdminUsers.Add(user);
                ctx.SaveChanges();
                return user.Id;
            }
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                var user = bs.GetAll().SingleOrDefault(u => u.PhoneNum == phoneNum);
                if(user == null)
                {
                    return false;
                }
                string dbHash = user.PasswordHash;
                string userHash = CommonHelper.CalcMD5(user.PasswordSalt + password);
                return userHash == dbHash;
            }
        }

        private AdminUserDTO ToDTO(AdminUserEntity user)
        {
            AdminUserDTO dto = new AdminUserDTO();
            dto.CityId = user.CityId;
            if (user.City != null)
            {
                //需要Include提升性能
                dto.CityName = user.City.Name;                
            }
            else
            {
                dto.CityName = "总部";
            }

            dto.CreateDateTime = user.CreateDateTime;
            dto.Email = user.Email;
            dto.Id = user.Id;
            dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.Name = user.Name;
            dto.PhoneNum = user.PhoneNum;
            return dto;
        }

        public AdminUserDTO[] GetAll(long? cityId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<AdminUserEntity> bs = new BaseService<AdminUserEntity>(ctx);
                return bs.GetAll().Include(u => u.City)
                    .AsNoTracking().ToList().Select(u => ToDTO(u)).ToArray();
            }
        }

        public AdminUserDTO[] GetAll()
        {
            throw new NotImplementedException();
        }

        public AdminUserDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public AdminUserDTO GetByPhoneNum(string phoneNum)
        {
            throw new NotImplementedException();
        }

        public bool HasPermission(long adminUserId, string permissionName)
        {
            throw new NotImplementedException();
        }

        public void MarkDeleted(long adminUserId)
        {
            throw new NotImplementedException();
        }

        public void RecordLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public void ResetLoginError(long id)
        {
            throw new NotImplementedException();
        }

        public long UpdateAdminUser(string name, string phoneNum, string password, string email, long? cityId)
        {
            throw new NotImplementedException();
        }
    }
}
