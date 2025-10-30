using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSimulator.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUnnecessaryFieldOnClassType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntensityLevel",
                table: "ClassTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntensityLevel",
                table: "ClassTypes",
                type: "INTEGER",
                nullable: true);
        }
    }
}
