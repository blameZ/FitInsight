using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitInsight.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNameToComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ActivityComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ActivityComments");
        }
    }
}
