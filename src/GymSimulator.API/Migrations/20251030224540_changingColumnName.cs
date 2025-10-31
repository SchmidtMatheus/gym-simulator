using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSimulator.API.Migrations
{
    /// <inheritdoc />
    public partial class changingColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyClassLimit",
                table: "PlanTypes",
                newName: "ClassLimit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassLimit",
                table: "PlanTypes",
                newName: "MonthlyClassLimit");
        }
    }
}
