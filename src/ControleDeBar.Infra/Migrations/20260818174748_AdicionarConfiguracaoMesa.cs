using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeBar.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarConfiguracaoMesa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas");

            migrationBuilder.RenameTable(
                name: "Mesas",
                newName: "TBMesa");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroLugares",
                table: "TBMesa",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "TBMesa",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBMesa",
                table: "TBMesa",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "UQ_TBMesa_Numero",
                table: "TBMesa",
                column: "Numero",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TBMesa",
                table: "TBMesa");

            migrationBuilder.DropIndex(
                name: "UQ_TBMesa_Numero",
                table: "TBMesa");

            migrationBuilder.RenameTable(
                name: "TBMesa",
                newName: "Mesas");

            migrationBuilder.AlterColumn<string>(
                name: "NumeroLugares",
                table: "Mesas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Mesas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mesas",
                table: "Mesas",
                column: "Id");
        }
    }
}
