using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
    public class HouseSearchResult
    {
        //当前页的数据
        public HouseDTO[] result { get; set; }
        //搜索结果的总条数
        public long totalCount { get; set; }
    }
}
