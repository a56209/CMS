using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using ZSZ.Service.Entities;

namespace ZSZ.Service.ModelConfig
{
    class AdminLogConfig:EntityTypeConfiguration<AdminLogEntity>
    {
        public AdminLogConfig()
        {
            ToTable("T_AdminLogs");
            //WillCascadeOnDelete  取消级联删除
            HasRequired(l => l.AdminUser).WithMany()
                .HasForeignKey(e => e.AdminUserId).WillCascadeOnDelete(false);
            Property(e => e.Message).IsRequired();
        }
    }
}
