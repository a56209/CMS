using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Service.Entities
{
    /// <summary>
    /// 后台操作日志
    /// </summary>
    public class AdminLogEntity : BaseEntity
    {
        /// <summary>
        /// 操作员编号
        /// </summary>
        public long AdminUserId{get;set;}
        /// <summary>
        /// 导航属性
        /// </summary>
        public virtual AdminUserEntity AdminUser { get; set; }
        /// <summary>
        /// 日志描述 
        /// </summary>
        public string Message{get;set;}        
    }
}