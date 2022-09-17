using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddPoolOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoolOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoolId = table.Column<int>(type: "int", nullable: false),
                    MaximumCap = table.Column<decimal>(type: "decimal(11,2)", precision: 11, scale: 2, nullable: false),
                    RosterSize = table.Column<int>(type: "int", nullable: false),
                    RequiredForwards = table.Column<int>(type: "int", nullable: false),
                    RequiredDefencemen = table.Column<int>(type: "int", nullable: false),
                    RequiredGoaltenders = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolOptions", x => x.Id);
                    table.ForeignKey(
                        name: "PoolOptions_Pool_FK1",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolOptions_PoolId",
                table: "PoolOptions",
                column: "PoolId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolOptions");
        }
    }
}
