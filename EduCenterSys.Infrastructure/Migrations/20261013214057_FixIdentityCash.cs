using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCenterSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixIdentityCash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            "ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF;",
            suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            "ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;",
            suppressTransaction: true);
        }
    }
}
