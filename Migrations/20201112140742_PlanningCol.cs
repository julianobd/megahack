using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaHack5.Migrations
{
    public partial class PlanningCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessOccupation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOccupation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maturity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maturity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planning",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvestmentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BusinessOccupationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaturityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planning_BusinessOccupation_BusinessOccupationId",
                        column: x => x.BusinessOccupationId,
                        principalTable: "BusinessOccupation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planning_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planning_Maturity_MaturityId",
                        column: x => x.MaturityId,
                        principalTable: "Maturity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planning_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<int>(type: "int", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanningId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_Planning_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InternalInvestment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvestmentValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlanningId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalInvestment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalInvestment_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternalInvestment_Planning_PlanningId",
                        column: x => x.PlanningId,
                        principalTable: "Planning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_PlanningId",
                table: "File",
                column: "PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalInvestment_DepartmentId",
                table: "InternalInvestment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalInvestment_PlanningId",
                table: "InternalInvestment",
                column: "PlanningId");

            migrationBuilder.CreateIndex(
                name: "IX_Planning_BusinessOccupationId",
                table: "Planning",
                column: "BusinessOccupationId");

            migrationBuilder.CreateIndex(
                name: "IX_Planning_CompanyId",
                table: "Planning",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Planning_MaturityId",
                table: "Planning",
                column: "MaturityId");

            migrationBuilder.CreateIndex(
                name: "IX_Planning_StatusId",
                table: "Planning",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "InternalInvestment");

            migrationBuilder.DropTable(
                name: "Planning");

            migrationBuilder.DropTable(
                name: "BusinessOccupation");

            migrationBuilder.DropTable(
                name: "Maturity");
        }
    }
}
