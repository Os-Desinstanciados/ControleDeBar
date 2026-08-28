using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeBar.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarIndicesPorUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TBProduto_Nome",
                table: "TBProduto");

            migrationBuilder.DropIndex(
                name: "UQ_TBMesa_Numero",
                table: "TBMesa");

            migrationBuilder.DropIndex(
                name: "UQ_TBGarcom_Nome",
                table: "TBGarcom");

            migrationBuilder.DropIndex(
                name: "UQ_TBCliente_Nome",
                table: "TBCliente");

            migrationBuilder.CreateIndex(
                name: "UQ_TBProduto_UserId_Nome",
                table: "TBProduto",
                columns: new[] { "UserId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBMesa_UserId_Numero",
                table: "TBMesa",
                columns: new[] { "UserId", "Numero" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBGarcom_UserId_Nome",
                table: "TBGarcom",
                columns: new[] { "UserId", "Nome" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBCliente_UserId_Nome",
                table: "TBCliente",
                columns: new[] { "UserId", "Nome" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_TBProduto_UserId_Nome",
                table: "TBProduto");

            migrationBuilder.DropIndex(
                name: "UQ_TBMesa_UserId_Numero",
                table: "TBMesa");

            migrationBuilder.DropIndex(
                name: "UQ_TBGarcom_UserId_Nome",
                table: "TBGarcom");

            migrationBuilder.DropIndex(
                name: "UQ_TBCliente_UserId_Nome",
                table: "TBCliente");

            migrationBuilder.CreateIndex(
                name: "UQ_TBProduto_Nome",
                table: "TBProduto",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBMesa_Numero",
                table: "TBMesa",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBGarcom_Nome",
                table: "TBGarcom",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBCliente_Nome",
                table: "TBCliente",
                column: "Nome",
                unique: true);
        }
    }
}
