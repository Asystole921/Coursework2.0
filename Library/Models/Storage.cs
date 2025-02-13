namespace Library.Models
{
    public class Storage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Capacity { get; set; }

        public string Interface { get; set; }

        public string FormFactor { get; set; }

        public int ReadSpeed { get; set; }

        public int WriteSpeed { get; set; }

        public double Price { get; set; }

        public string? ImgUrl { get; set; }
    }
}
