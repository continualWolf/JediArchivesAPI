using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JediArchives.Migrations;

/// <inheritdoc />
public partial class AddGalaxyMappingModels : Migration {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "Polygons",
            columns: table => new {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
            },
            constraints: table => {
                table.PrimaryKey("PK_Polygons", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CoordinatePoints",
            columns: table => new {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                X = table.Column<float>(type: "real", nullable: false),
                Y = table.Column<float>(type: "real", nullable: false),
                Order = table.Column<int>(type: "int", nullable: false),
                PolygonId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_CoordinatePoints", x => x.Id);
                table.ForeignKey(
                    name: "FK_CoordinatePoints_Polygons_PolygonId",
                    column: x => x.PolygonId,
                    principalTable: "Polygons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Regions",
            columns: table => new {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PolygonId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_Regions", x => x.Id);
                table.ForeignKey(
                    name: "FK_Regions_Polygons_PolygonId",
                    column: x => x.PolygonId,
                    principalTable: "Polygons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "Sectors",
            columns: table => new {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                RegionId = table.Column<int>(type: "int", nullable: false),
                PolygonId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table => {
                table.PrimaryKey("PK_Sectors", x => x.Id);
                table.ForeignKey(
                    name: "FK_Sectors_Polygons_PolygonId",
                    column: x => x.PolygonId,
                    principalTable: "Polygons",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
                table.ForeignKey(
                    name: "FK_Sectors_Regions_RegionId",
                    column: x => x.RegionId,
                    principalTable: "Regions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Systems",
            columns: table => new {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                X = table.Column<float>(type: "real", nullable: false),
                Y = table.Column<float>(type: "real", nullable: false),
                SectorId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_Systems", x => x.Id);
                table.ForeignKey(
                    name: "FK_Systems_Sectors_SectorId",
                    column: x => x.SectorId,
                    principalTable: "Sectors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Planets",
            columns: table => new {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Allegiance = table.Column<int>(type: "int", nullable: false),
                OrbitX = table.Column<float>(type: "real", nullable: true),
                OrbitY = table.Column<float>(type: "real", nullable: true),
                SystemEntityId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table => {
                table.PrimaryKey("PK_Planets", x => x.Id);
                table.ForeignKey(
                    name: "FK_Planets_Systems_SystemEntityId",
                    column: x => x.SystemEntityId,
                    principalTable: "Systems",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CoordinatePoints_PolygonId_Order",
            table: "CoordinatePoints",
            columns: new[] { "PolygonId", "Order" });

        migrationBuilder.CreateIndex(
            name: "IX_Planets_SystemEntityId",
            table: "Planets",
            column: "SystemEntityId");

        migrationBuilder.CreateIndex(
            name: "IX_Regions_PolygonId",
            table: "Regions",
            column: "PolygonId");

        migrationBuilder.CreateIndex(
            name: "IX_Sectors_PolygonId",
            table: "Sectors",
            column: "PolygonId");

        migrationBuilder.CreateIndex(
            name: "IX_Sectors_RegionId",
            table: "Sectors",
            column: "RegionId");

        migrationBuilder.CreateIndex(
            name: "IX_Systems_SectorId",
            table: "Systems",
            column: "SectorId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(
            name: "CoordinatePoints");

        migrationBuilder.DropTable(
            name: "Planets");

        migrationBuilder.DropTable(
            name: "Systems");

        migrationBuilder.DropTable(
            name: "Sectors");

        migrationBuilder.DropTable(
            name: "Regions");

        migrationBuilder.DropTable(
            name: "Polygons");
    }
}
