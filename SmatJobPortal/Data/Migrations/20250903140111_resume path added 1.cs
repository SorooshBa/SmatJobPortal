using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmatJobPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class resumepathadded1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resume",
                table: "JobApply");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "JobApply",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
