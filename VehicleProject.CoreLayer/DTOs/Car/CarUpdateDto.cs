namespace VehicleProject.CoreLayer.DTOs.Car
{
    public class CarUpdateDto
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsHeadlightOn { get; set; }
        public int ColorId { get; set; }
        public int NumberOfWheels { get; set; }
    }
}
