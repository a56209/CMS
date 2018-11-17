using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;

namespace ZSZ.IService
{
    public interface IIdNameService:IServiceSupport
    {
        long AddNew(string typeName, string name);
        IdNameDTO GetbyId(long id);
        /// <summary>
        /// 获取类别下的IdName(比如所有民族)
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        IdNameDTO[] GetAll(string typeName);
    }
}
