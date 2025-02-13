namespace Library.Models
{
    public class CPU
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Architecture { get; set; }

        public double BaseClock { get; set; }

        public double BoostClock { get; set; }

        public int Cores { get; set; }

        public int Threads { get; set; }

        public double TDP { get; set; }

        public int SocketId { get; set; }

        public double Price { get; set; }

        public string? ImgUrl { get; set; }
    }
}