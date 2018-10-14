using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Service.Entities
{
    /// <summary>
    /// 房屋配套设施
    /// </summary>
    public class AttachmentEntity : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// 图标图片
        /// </summary>
        public string IconName { get; set; }
        public virtual ICollection<HouseEntity> Houses { get; set; } = new List<HouseEntity>();
    }
}
