using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDev.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDI1CurvaCorrecaoForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DI1Curva_ReferenciaCurva_ReferenciaCurvaId",
                table: "DI1Curva");

            migrationBuilder.DropIndex(
                name: "IX_DI1Curva_ReferenciaCurvaId",
                table: "DI1Curva");

            migrationBuilder.DropColumn(
                name: "ReferenciaCurvaId",
                table: "DI1Curva");

            migrationBuilder.CreateIndex(
                name: "IX_DI1Curva_IdReferenciaCurva",
                table: "DI1Curva",
                column: "IdReferenciaCurva");

            migrationBuilder.AddForeignKey(
                name: "FK_DI1Curva_ReferenciaCurva_IdReferenciaCurva",
                table: "DI1Curva",
                column: "IdReferenciaCurva",
                principalTable: "ReferenciaCurva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DI1Curva_ReferenciaCurva_IdReferenciaCurva",
                table: "DI1Curva");

            migrationBuilder.DropIndex(
                name: "IX_DI1Curva_IdReferenciaCurva",
                table: "DI1Curva");

            migrationBuilder.AddColumn<int>(
                name: "ReferenciaCurvaId",
                table: "DI1Curva",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DI1Curva_ReferenciaCurvaId",
                table: "DI1Curva",
                column: "ReferenciaCurvaId");

            migrationBuilder.AddForeignKey(
                name: "FK_DI1Curva_ReferenciaCurva_ReferenciaCurvaId",
                table: "DI1Curva",
                column: "ReferenciaCurvaId",
                principalTable: "ReferenciaCurva",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
