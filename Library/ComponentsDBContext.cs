using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public class ComponentsDBContext : DbContext
    {
        public ComponentsDBContext()
            : base()
        {
        }

        public DbSet<Build> Builds { get; set; }

        public DbSet<CPU> CPUs { get; set; }

        public DbSet<CPUCooler> CPUCoolers { get; set; }

        public DbSet<GPU> GPUs { get; set; }

        public DbSet<Motherboard> Motherboards { get; set; }

        public DbSet<PCCase> PCCases { get; set; }

        public DbSet<PowerSupply> PowerSupplies { get; set; }

        public DbSet<RAM> RAMs { get; set; }

        public DbSet<RAMType> RAMTypes { get; set; }

        public DbSet<Socket> Sockets { get; set; }

        public DbSet<Storage> Storages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\user\\Desktop\\KursWork(ver1.0)\\KursWork\\DB\\ComponentsDB.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True");
        }
    }
}
