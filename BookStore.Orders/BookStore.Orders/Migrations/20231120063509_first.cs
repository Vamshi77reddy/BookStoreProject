using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Orders.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    UserID = table.Column<long>(nullable: false),
                    BookID = table.Column<long>(nullable: false),
                    OrderQty = table.Column<long>(nullable: false),
                    OrderAmount = table.Column<float>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    IsSuccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
