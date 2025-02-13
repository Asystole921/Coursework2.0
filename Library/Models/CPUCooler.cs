namespace Library.Models
{
    public class CPUCooler
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public int SocketId { get; set; }

        public int FanSpeed { get; set; }

        public double NoiseLevel { get; set; }

        public double Airflow { get; set; }

        public string Dimensions { get; set; }

        public string? ImgUrl { get; set; }

        public double Price { get; set; }
    }
}
