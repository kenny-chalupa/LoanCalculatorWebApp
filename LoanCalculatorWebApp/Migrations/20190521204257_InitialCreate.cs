using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoanCalculatorWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LoanAmount = table.Column<double>(maxLength: 64, nullable: false),
                    LoanTerm = table.Column<int>(maxLength: 64, nullable: false),
                    InterestRates = table.Column<double>(nullable: false),
                    NameOfLoan = table.Column<string>(nullable: true),
                    TotalCost = table.Column<double>(nullable: false),
                    MonthlyBill = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanData");
        }
    }
}
