using DataImport.Storage.Core;

namespace DataImport.Core.Domain
{
    public class VendorPriceItem : IEntity<long>
    {
        public long InternalId { get; set; }
        
        public string ProviderId { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public string ShortNumber { get; set; }
        public string Description { get; set; }
        public decimal DeliveryTerm { get; set; }
        public decimal Price { get; set; }
        public decimal PriceOne { get; set; }
        public decimal PriceTwo { get; set; }
        public decimal PriceThree { get; set; }
        public decimal PriceFour { get; set; }
        public decimal PriceFive { get; set; }
        public decimal PriceSix { get; set; }
        public decimal PriceSeven { get; set; }
        public decimal PriceEight { get; set; }
        public decimal PriceNine { get; set; }
        public decimal PriceTen { get; set; }
        public decimal PriceEleven { get; set; }
        public decimal PriceTwelve { get; set; }
        public decimal PriceThirteen { get; set; }
        public decimal PriceFourteen { get; set; }
        public decimal PriceFifteen { get; set; }
        public decimal PriceSixteen { get; set; }
        public decimal PriceSeventeen { get; set; }
        
        public string BranchOne { get; set; }
        public string BranchTwo { get; set; }
        public string BranchThree { get; set; }
        public string BranchFour { get; set; }
        public string BranchFive { get; set; }
        public string BranchSix { get; set; }
        public string BranchSeven { get; set; }
        public string Availability { get; set; }
        public string VendorNumber { get; set; }
        
        public int VendorId { get; set; }
        public int WarehouseId { get; set; }
    }
}