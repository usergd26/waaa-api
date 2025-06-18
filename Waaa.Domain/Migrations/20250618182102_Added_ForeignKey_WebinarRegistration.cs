using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Waaa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Added_ForeignKey_WebinarRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WebinarRegistrations_WebinarId",
                table: "WebinarRegistrations",
                column: "WebinarId");

            migrationBuilder.AddForeignKey(
                name: "FK_WebinarRegistrations_Webinars_WebinarId",
                table: "WebinarRegistrations",
                column: "WebinarId",
                principalTable: "Webinars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebinarRegistrations_Webinars_WebinarId",
                table: "WebinarRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_WebinarRegistrations_WebinarId",
                table: "WebinarRegistrations");
        }
    }
}
