using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.Service.Entities
{
    /// <summary>
    /// 设置（键值对）
    /// </summary>
    public class SettingEntity:BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
