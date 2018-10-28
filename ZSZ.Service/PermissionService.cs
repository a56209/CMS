using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    public class PermissionService : IPermissionService
    {
        public void AddPermIds(long roleId, long[] permIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> roleBS = new BaseService<RoleEntity>(ctx);
                var role = roleBS.GetById(roleId);
                if (role == null)
                {
                    throw new ArgumentException("roleId不存在" + roleId);
                }
                BaseService<PermissionEntity> permBS = new BaseService<PermissionEntity>(ctx);
                var perms = permBS.GetAll().Where(p => permIds.Contains(p.Id)).ToArray();
                foreach (var item in perms)
                {
                    role.Permissions.Add(item);
                }
                ctx.SaveChanges();
            }                
        }

        public long AddPermission(string permName, string description)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> permBS = new BaseService<PermissionEntity>(ctx);
                bool exists = permBS.GetAll().Any(p => p.Name == permName);
                if(exists)
                {
                    throw new ArgumentException("权限项已经存在");
                }
                PermissionEntity perm = new PermissionEntity();
                perm.Description = description;
                perm.Name = permName;
                ctx.Permissions.Add(perm);
                ctx.SaveChanges();
                return perm.Id;
            }
        }

        private PermissionDTO ToDTO(PermissionEntity p)
        {
            PermissionDTO dto = new PermissionDTO();
            dto.CreateDateTime = p.CreateDateTime;
            dto.Description = p.Description;
            dto.Id = p.Id;
            dto.Name = p.Name;
            return dto;
        }

        public PermissionDTO[] GerByRoleId(long roleId)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                return bs.GetById(roleId).Permissions.ToList().Select(p => ToDTO(p)).ToArray();
            }                
        }

        public PermissionDTO[] GetAll()
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {                
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                return bs.GetAll().ToList().Select(p => ToDTO(p)).ToArray();
            }                
        }

        public PermissionDTO GetById(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                var pe = bs.GetById(id);
                return pe == null ? null : ToDTO(pe);
            }
        }

        public PermissionDTO GetByName(string name)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                var pe = bs.GetAll().SingleOrDefault(p => p.Name == name);
                //三元运算符
                return pe == null ? null : ToDTO(pe);
            }
        }

        public void MarkDeleted(long id)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                bs.MarkDeleted(id);
            }                
        }

        public void UpdatePermIds(long roleId, long[] permIds)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<RoleEntity> bs = new BaseService<RoleEntity>(ctx);
                var role = bs.GetById(roleId);
                if(role == null)
                {
                    throw new ArgumentException("roleId不存在" + roleId);
                }
                role.Permissions.Clear();
                BaseService<PermissionEntity> permbs = new BaseService<PermissionEntity>(ctx);                
                var perms = permbs.GetAll().Where(p => permIds.Contains(p.Id)).ToList();
                foreach (var item in perms)
                {
                    role.Permissions.Add(item);
                }
                ctx.SaveChanges();
            }
        }

        public void UpdatePermission(long id, string permName, string description)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                BaseService<PermissionEntity> bs = new BaseService<PermissionEntity>(ctx);
                var perm = bs.GetById(id);
                if (perm == null)
                {
                    throw new ArgumentException("id不存在" + id);
                }
                perm.Name = permName;
                perm.Description = description;
                ctx.SaveChanges();
            }
        }
    }
}
