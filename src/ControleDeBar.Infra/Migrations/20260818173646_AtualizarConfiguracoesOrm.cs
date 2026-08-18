using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeBar.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarConfiguracoesOrm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Garcons",
                table: "Garcons");

            migrationBuilder.DropColumn(
                name: "MesaId",
                table: "Mesas");

            migrationBuilder.RenameTable(
                name: "Produtos",
                newName: "TBProduto");

            migrationBuilder.RenameTable(
                name: "Garcons",
                newName: "TBGarcom");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "TBProduto",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TBProduto",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "TBGarcom",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBProduto",
                table: "TBProduto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBGarcom",
                table: "TBGarcom",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "UQ_TBProduto_Nome",
                table: "TBProduto",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TBGarcom_Nome",
                table: "TBGarcom",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TBProduto",
                table: "TBProduto");

            migrationBuilder.DropIndex(
                name: "UQ_TBProduto_Nome",
                table: "TBProduto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBGarcom",
                table: "TBGarcom");

            migrationBuilder.DropIndex(
                name: "UQ_TBGarcom_Nome",
                table: "TBGarcom");

            migrationBuilder.RenameTable(
                name: "TBProduto",
                newName: "Produtos");

            migrationBuilder.RenameTable(
                name: "TBGarcom",
                newName: "Garcons");

            migrationBuilder.AddColumn<Guid>(
                name: "MesaId",
                table: "Mesas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Garcons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Produtos",
                table: "Produtos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Garcons",
                table: "Garcons",
                column: "Id");
        }
    }
}
