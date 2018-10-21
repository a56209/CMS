using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface IAdminLogService:IServiceSupport
    {
        /// <summary>
        /// 插入一条日志
        /// </summary>
        /// <param name="adminUserId">操作用户Id</param>
        /// <param name="message">消息</param>
        /// <returns></returns>
        long AddNew(long adminUserId, string message);

        //TODO:日志搜索
        AdminLogDTO GetById(long Id);
    }
}
