using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImport.Storage.Migrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VendorPriceItems",
                columns: table => new
                {
                    InternalId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    ShortNumber = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(80)", nullable: true),
                    DeliveryTerm = table.Column<decimal>(type: "decimal(9,1)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceOne = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceTwo = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceThree = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceFour = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceFive = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceSix = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceSeven = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceEight = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceNine = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceTen = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceEleven = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceTwelve = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceThirteen = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceFourteen = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceFifteen = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceSixteen = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    PriceSeventeen = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    BranchOne = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    BranchTwo = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    BranchThree = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    BranchFour = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    BranchFive = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    BranchSix = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    BranchSeven = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Availability = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    VendorNumber = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    VendorId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorPriceItems", x => x.InternalId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VendorPriceItems");
        }
    }
}
