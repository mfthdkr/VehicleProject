namespace VehicleProject.CoreLayer.DTOs.Bus
{
    public class BusCreateDto
    {
        public int NumberOfWheels { get; set; }
        public int ColorId { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsHeadlightOn { get; set; }
    }
}
