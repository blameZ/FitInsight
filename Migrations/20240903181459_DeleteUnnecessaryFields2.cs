using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitInsight.Migrations
{
    /// <inheritdoc />
    public partial class DeleteUnnecessaryFields2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_Id",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityLikes_AspNetUsers_Id",
                table: "ActivityLikes");

            migrationBuilder.DropIndex(
                name: "IX_ActivityLikes_Id",
                table: "ActivityLikes");

            migrationBuilder.DropIndex(
                name: "IX_Activities_Id",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActivityLikes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Activities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ActivityLikes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Activities",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLikes_Id",
                table: "ActivityLikes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Id",
                table: "Activities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_Id",
                table: "Activities",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityLikes_AspNetUsers_Id",
                table: "ActivityLikes",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
