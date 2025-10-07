using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceDev.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableDI1CurvaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferenciaCurva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataReferencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenciaCurva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DI1Curva",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Vencimento = table.Column<string>(type: "TEXT", nullable: false),
                    Ajuste = table.Column<double>(type: "REAL", nullable: false),
                    IdReferenciaCurva = table.Column<int>(type: "INTEGER", nullable: false),
                    ReferenciaCurvaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DI1Curva", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DI1Curva_ReferenciaCurva_ReferenciaCurvaId",
                        column: x => x.ReferenciaCurvaId,
                        principalTable: "ReferenciaCurva",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DI1Curva_ReferenciaCurvaId",
                table: "DI1Curva",
                column: "ReferenciaCurvaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DI1Curva");

            migrationBuilder.DropTable(
                name: "ReferenciaCurva");
        }
    }
}
