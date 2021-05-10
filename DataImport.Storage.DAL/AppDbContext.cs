using DataImport.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace DataImport.Storage.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        #region DbSets
        public DbSet<VendorPriceItem> VendorPriceItems { get; set; }
        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendorPriceItem>().HasKey(x => x.InternalId);
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.Price).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceOne).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceTwo).HasColumnType("decimal(9,2)"); 
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceThree).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceFour).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceFive).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceSix).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceSeven).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceEight).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceNine).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceTen).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceEleven).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceTwelve).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceThirteen).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceFourteen).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceFifteen).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceSixteen).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.PriceSeventeen).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.DeliveryTerm).HasColumnType("decimal(9,1)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.Brand).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.Number).HasColumnType("nvarchar(25)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.ShortNumber).HasColumnType("nvarchar(25)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.Description).HasColumnType("nvarchar(80)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.Availability).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.BranchOne).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.BranchTwo).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.BranchThree).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.BranchFour).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.BranchFive).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.BranchSix).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.BranchSeven).HasColumnType("nvarchar(10)");
            modelBuilder.Entity<VendorPriceItem>()
                .Property(p => p.VendorNumber).HasColumnType("nvarchar(25)");
            
        }
    }
}