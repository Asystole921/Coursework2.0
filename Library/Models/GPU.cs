namespace Library.Models
{
    public class GPU
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Chipset { get; set; }

        public int MemoryCapacity { get; set; }

        public string MemoryType { get; set; }

        public float TDP { get; set; }

        public float BaseClock { get; set; }

        public float BoostClock { get; set; }

        public string Architecture { get; set; }

        public double Price { get; set; }

        public string ImgUrl { get; set; }
    }
}
