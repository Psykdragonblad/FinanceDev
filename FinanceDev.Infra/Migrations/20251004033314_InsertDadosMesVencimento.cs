using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceDev.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InsertDadosMesVencimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MesVencimento",
                columns: new[] { "Id", "Codigo", "Mes" },
                values: new object[,]
                {
                    { 1, "F", "Janeiro" },
                    { 2, "G", "Fevereiro" },
                    { 3, "H", "Março" },
                    { 4, "J", "Abril" },
                    { 5, "K", "Maio" },
                    { 6, "M", "Junho" },
                    { 7, "N", "Julho" },
                    { 8, "Q", "Agosto" },
                    { 9, "U", "Setembro" },
                    { 10, "V", "Outubro" },
                    { 11, "X", "Novembro" },
                    { 12, "Z", "Dezembro" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MesVencimento",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
