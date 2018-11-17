using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface ICommunityService:IServiceSupport
    {
        /// <summary>
        /// 获取区域regionId下所有小区
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        CommunityDTO[] GetByRegionId(long regionId);
    }
}
