using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface IRegionService:IServiceSupport
    {
        RegionDTO GetById(long id);
        /// <summary>
        /// 获取城市下的区域
        /// </summary>
        /// <param name="cityId">城市编号</param>
        /// <returns></returns>
        RegionDTO[] GetAll(long cityId);
    }
}
