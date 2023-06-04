namespace VehicleProject.CoreLayer.Entities.Abstract
{
    public interface IBaseEntity 
    {
         DateTime CreatedDate { get; set; }
         DateTime? DeletedDate { get; set; }
         DateTime? UpdatedDate { get; set; }
         bool IsDeleted { get; set; }
         bool IsUpdated { get; set; }
    }
}
