using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PoolSequence");

            migrationBuilder.CreateSequence(
                name: "PoolTeamSequence");

            migrationBuilder.CreateTable(
                name: "Pools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR PoolSequence"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoolTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR PoolTeamSequence"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PoolId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoolTeams_Pools_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DraftPick",
                columns: table => new
                {
                    PoolTeamId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Round = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftPick", x => new { x.PoolTeamId, x.Id });
                    table.ForeignKey(
                        name: "FK_DraftPick_PoolTeams_PoolTeamId",
                        column: x => x.PoolTeamId,
                        principalTable: "PoolTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Position_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Team_Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    PoolTeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_PoolTeams_PoolTeamId",
                        column: x => x.PoolTeamId,
                        principalTable: "PoolTeams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_PoolTeamId",
                table: "Player",
                column: "PoolTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolTeams_PoolId",
                table: "PoolTeams",
                column: "PoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraftPick");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "PoolTeams");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropSequence(
                name: "PoolSequence");

            migrationBuilder.DropSequence(
                name: "PoolTeamSequence");
        }
    }
}
