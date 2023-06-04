namespace VehicleProject.CoreLayer.DTOs.Common
{
    public abstract class VehicleBaseDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsHeadlightOn { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime? DeletedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUpdated { get; set; }
    }
}
