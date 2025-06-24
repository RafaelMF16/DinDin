using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DinDin.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthlySummaryAndTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlySummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    TotalIncome = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalExpense = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlySummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlySummaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IncomeCategory = table.Column<int>(type: "integer", nullable: false),
                    ExpenseCategory = table.Column<int>(type: "integer", nullable: false),
                    Amont = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MonthlySummaryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_MonthlySummaries_MonthlySummaryId",
                        column: x => x.MonthlySummaryId,
                        principalTable: "MonthlySummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlySummaries_UserId",
                table: "MonthlySummaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_MonthlySummaryId",
                table: "Transactions",
                column: "MonthlySummaryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "MonthlySummaries");
        }
    }
}
