using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmatJobPortal.Data.Migrations
{
    /// <inheritdoc />
    public partial class useraddedtojob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployerUserId",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_EmployerUserId",
                table: "Jobs",
                column: "EmployerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerUserId",
                table: "Jobs",
                column: "EmployerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AspNetUsers_EmployerUserId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_EmployerUserId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "EmployerUserId",
                table: "Jobs");
        }
    }
}
