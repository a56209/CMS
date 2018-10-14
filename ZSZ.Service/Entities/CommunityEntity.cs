using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Service.Entities
{
    /// <summary>
    /// 小区
    /// </summary>
    public class CommunityEntity:BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 区域编号
        /// </summary>
        public long RegionId { set; get; }

        public virtual RegionEntity Region { set; get; }
        /// <summary>
        /// 位置
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 交通描述
        /// </summary>
        public string Traffic { get; set; }
        /// <summary>
        /// 建造年份
        /// </summary>
        public int? BuiltYear { get; set; }
    }
}
