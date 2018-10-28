using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface IAttachmentService:IServiceSupport
    {
        /// <summary>
        /// 获取所有的设施
        /// </summary>
        /// <returns></returns>
        AttachmentDTO[] GetAll();
        /// <summary>
        /// 获取房子houseId有用的设施
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        AttachmentDTO[] GetAttachments(long houseId);
    }
}
