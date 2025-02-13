namespace Library.Models
{
    public class RAM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public int RAMTypeId { get; set; }

        public int Frequency { get; set; }

        public int Capacity { get; set; }

        public int Modules { get; set; }

        public int CASLatency { get; set; }

        public double Price { get; set; }

        public string? ImgUrl { get; set; }
    }
}
