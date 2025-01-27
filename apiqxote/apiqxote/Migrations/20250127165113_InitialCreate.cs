using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiqxote.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bio_concentration",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    species = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    bcf = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    cf = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    r = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    ctree = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bio_concentration", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tree_name",
                columns: table => new
                {
                    tree_name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    coordinate_count = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tree_name", x => x.tree_name);
                });

            migrationBuilder.CreateTable(
                name: "zone",
                columns: table => new
                {
                    zone = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    area = table.Column<int>(type: "int", nullable: true),
                    classification = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    plots = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zone", x => x.zone);
                });

            migrationBuilder.CreateTable(
                name: "animal",
                columns: table => new
                {
                    animal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    temperature = table.Column<double>(type: "float", nullable: true),
                    cloud_cover = table.Column<int>(type: "int", nullable: true),
                    wind_speed = table.Column<int>(type: "int", nullable: true),
                    species_name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    coordinates = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    abudance = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    coverboards = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    zone = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animal", x => x.animal_id);
                    table.ForeignKey(
                        name: "fk_animal_zone1",
                        column: x => x.zone,
                        principalTable: "zone",
                        principalColumn: "zone");
                });

            migrationBuilder.CreateTable(
                name: "plant",
                columns: table => new
                {
                    plant_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plot_nr = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    coordinate = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    species = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    cover = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    temperature = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    humidity = table.Column<float>(type: "real", nullable: true),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    zone = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plant", x => x.plant_id);
                    table.ForeignKey(
                        name: "fk_plant_zone1",
                        column: x => x.zone,
                        principalTable: "zone",
                        principalColumn: "zone");
                });

            migrationBuilder.CreateTable(
                name: "tree",
                columns: table => new
                {
                    tree_nr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    zone = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    coordinate = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    height = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    circumference = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    volume = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    bio_concentration_id = table.Column<int>(type: "int", nullable: false),
                    tree_name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tree", x => new { x.tree_nr, x.zone });
                    table.ForeignKey(
                        name: "fk_tree_bio_concentration1",
                        column: x => x.bio_concentration_id,
                        principalTable: "bio_concentration",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tree_tree_name1",
                        column: x => x.tree_name,
                        principalTable: "tree_name",
                        principalColumn: "tree_name");
                    table.ForeignKey(
                        name: "fk_tree_zone1",
                        column: x => x.zone,
                        principalTable: "zone",
                        principalColumn: "zone");
                });

            migrationBuilder.CreateIndex(
                name: "fk_animal_zone1",
                table: "animal",
                column: "zone");

            migrationBuilder.CreateIndex(
                name: "fk_plant_zone1",
                table: "plant",
                column: "zone");

            migrationBuilder.CreateIndex(
                name: "fk_tree_bio_concentration1",
                table: "tree",
                column: "bio_concentration_id");

            migrationBuilder.CreateIndex(
                name: "fk_tree_tree_name1",
                table: "tree",
                column: "tree_name");

            migrationBuilder.CreateIndex(
                name: "fk_tree_zone1",
                table: "tree",
                column: "zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "animal");

            migrationBuilder.DropTable(
                name: "plant");

            migrationBuilder.DropTable(
                name: "tree");

            migrationBuilder.DropTable(
                name: "bio_concentration");

            migrationBuilder.DropTable(
                name: "tree_name");

            migrationBuilder.DropTable(
                name: "zone");
        }
    }
}
