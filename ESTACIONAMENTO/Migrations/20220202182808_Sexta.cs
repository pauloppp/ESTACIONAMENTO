using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ESTACIONAMENTO.Migrations
{
    public partial class Sexta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manobras2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Classificacao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarroId = table.Column<int>(type: "int", nullable: false),
                    ManobristaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manobras2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manobras2_Carros_CarroId",
                        column: x => x.CarroId,
                        principalTable: "Carros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Manobras2_Manobristas_ManobristaId",
                        column: x => x.ManobristaId,
                        principalTable: "Manobristas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manobras_ManobristaId",
                table: "Manobras",
                column: "ManobristaId");

            migrationBuilder.CreateIndex(
                name: "IX_Manobras2_CarroId",
                table: "Manobras2",
                column: "CarroId");

            migrationBuilder.CreateIndex(
                name: "IX_Manobras2_ManobristaId",
                table: "Manobras2",
                column: "ManobristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manobras_Manobristas_ManobristaId",
                table: "Manobras",
                column: "ManobristaId",
                principalTable: "Manobristas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manobras_Manobristas_ManobristaId",
                table: "Manobras");

            migrationBuilder.DropTable(
                name: "Manobras2");

            migrationBuilder.DropIndex(
                name: "IX_Manobras_ManobristaId",
                table: "Manobras");
        }
    }
}
