using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training_Session_Booking_Portal.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovedToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Sessions");
        }
    }
}
