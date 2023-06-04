namespace VehicleProject.CoreLayer.DTOs.Boat
{
    public class BoatCreateDto
    {
        public int ColorId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsHeadlightOn { get; set; }
    }
}
