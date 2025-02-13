namespace Library.Models
{
    public class PCCase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Type { get; set; }

        public string Color { get; set; }

        public string Dimensions { get; set; }

        public string FormFactor { get; set; }

        public double Price { get; set; }

        public string? ImgUrl { get; set; }
    }
}
