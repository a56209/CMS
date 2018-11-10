using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface ICityService:IServiceSupport
    {
        long AddNewe(string cityName);

        /// <summary>
        /// 根据Id获取城市DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CityDTO GetById(long id);

        CityDTO[] GetAll();
    }
}
