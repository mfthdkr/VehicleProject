namespace VehicleProject.CoreLayer.DTOs.Boat
{
    public class BoatUpdateDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsHeadlightOn { get; set; }
        public int ColorId { get; set; }
    }
}
