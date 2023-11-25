using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infra.Orm.Migrations
{
    /// <inheritdoc />
    public partial class minhaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBCirurgia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(300)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraTermino = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBCirurgia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(300)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CRM = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBConsulta",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(300)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraTermino = table.Column<TimeSpan>(type: "time", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBConsulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBConsulta_TBMedico_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "TBMedico",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TBMedicoCirurgia",
                columns: table => new
                {
                    CirurgiasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MedicosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedicoCirurgia", x => new { x.CirurgiasId, x.MedicosId });
                    table.ForeignKey(
                        name: "FK_TBMedicoCirurgia_TBCirurgia_CirurgiasId",
                        column: x => x.CirurgiasId,
                        principalTable: "TBCirurgia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBMedicoCirurgia_TBMedico_MedicosId",
                        column: x => x.MedicosId,
                        principalTable: "TBMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBConsulta_MedicoId",
                table: "TBConsulta",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_TBMedicoCirurgia_MedicosId",
                table: "TBMedicoCirurgia",
                column: "MedicosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBConsulta");

            migrationBuilder.DropTable(
                name: "TBMedicoCirurgia");

            migrationBuilder.DropTable(
                name: "TBCirurgia");

            migrationBuilder.DropTable(
                name: "TBMedico");
        }
    }
}
