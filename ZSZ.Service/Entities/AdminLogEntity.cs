using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Service.Entities
{
    /// <summary>
    /// ��̨������־
    /// </summary>
    public class AdminLogEntity : BaseEntity
    {
        /// <summary>
        /// ����Ա���
        /// </summary>
        public long AdminUserId{get;set;}
        /// <summary>
        /// ��������
        /// </summary>
        public virtual AdminUserEntity AdminUser { get; set; }
        /// <summary>
        /// ��־���� 
        /// </summary>
        public string Message{get;set;}        
    }
}