using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface IPermissionService:IServiceSupport
    {
        long AddPermission(string permName, string description);
        void UpdatePermission(long id, string permName, string description);
        void MarkDeleted(long id);
        PermissionDTO GetById(long id);
        PermissionDTO[] GetAll();
        PermissionDTO GetByName(string name);
        //获取角色的权限
        PermissionDTO[] GerByRoleId(long roleId);
        //给角色roleId增加权限
        void AddPermIds(long roleId, long[] permIds);
        void UpdatePermIds(long roleId, long[] permIds);
    }
}
