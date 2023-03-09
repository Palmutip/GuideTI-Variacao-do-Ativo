using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuideTI_Variacao_do_Ativo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Variacoes",
                columns: table => new
                {
                    Dia = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false),
                    VaricaoRelacaoD1 = table.Column<string>(type: "TEXT", nullable: false),
                    VariacaoRelacaoPrimeiraData = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variacoes", x => x.Dia);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variacoes");
        }
    }
}
