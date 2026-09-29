using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCenterSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGradeLevelEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Grades");

            migrationBuilder.AddColumn<byte>(
                name: "Level",
                table: "Grades",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Grades");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Grades",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
