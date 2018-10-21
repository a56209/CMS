using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZSZ.DTO
{
    public class HouseAppointmentDTO:BaseDTO
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public DateTime VisitDate { get; set; }
        public long HouseId { get; set; }
        public string Status { get; set; }
        public long? FolloAdminUserId { get; set; }
        public String FollowAdminUserName { get; set; }
        public DateTime? FollowDateTime { get; set; }
        public String RegionName { get; set; }
        public String CommunityName { get; set; }
        public String HouseAddress { get; set; }
    }
}
