using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.IService
{
    public interface ICityService:IServiceSupport
    {
        long AddNewe(string cityName);
    }
}
