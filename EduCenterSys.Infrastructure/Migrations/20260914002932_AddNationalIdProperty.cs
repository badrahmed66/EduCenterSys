using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCenterSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNationalIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Teachers",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_NationalId",
                table: "Teachers",
                column: "NationalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teachers_NationalId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Teachers");
        }
    }
}
