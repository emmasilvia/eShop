using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eShop.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "listaIngrediente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaIngrediente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "listaRestaurante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaRestaurante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "listaAdrese",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Strada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Oras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaAdrese", x => x.Id);
                    table.ForeignKey(
                        name: "FK_listaAdrese_listaRestaurante_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "listaRestaurante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "listaProduse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Denumire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pret = table.Column<double>(type: "float", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaProduse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_listaProduse_listaRestaurante_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "listaRestaurante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "listaProduse_Ingrediente",
                columns: table => new
                {
                    ProdusId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_listaProduse_Ingrediente", x => new { x.ProdusId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_listaProduse_Ingrediente_listaIngrediente_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "listaIngrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_listaProduse_Ingrediente_listaProduse_ProdusId",
                        column: x => x.ProdusId,
                        principalTable: "listaProduse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_listaAdrese_RestaurantId",
                table: "listaAdrese",
                column: "RestaurantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_listaProduse_RestaurantId",
                table: "listaProduse",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_listaProduse_Ingrediente_IngredientId",
                table: "listaProduse_Ingrediente",
                column: "IngredientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "listaAdrese");

            migrationBuilder.DropTable(
                name: "listaProduse_Ingrediente");

            migrationBuilder.DropTable(
                name: "listaIngrediente");

            migrationBuilder.DropTable(
                name: "listaProduse");

            migrationBuilder.DropTable(
                name: "listaRestaurante");
        }
    }
}
