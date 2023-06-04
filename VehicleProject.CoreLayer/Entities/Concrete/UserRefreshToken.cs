using System.ComponentModel.DataAnnotations;
using VehicleProject.CoreLayer.Entities.Abstract;

namespace VehicleProject.CoreLayer.Entities.Concrete
{
    public class UserRefreshToken: IBaseEntity
    {
        [Key]
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
    }
}
