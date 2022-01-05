using Microsoft.EntityFrameworkCore.Migrations;

namespace Honoured.Migrations
{
    public partial class AddedDimensiontotier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MaxDimensionId",
                table: "AppSubscriptionTiers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSubscriptionTiers_MaxDimensionId",
                table: "AppSubscriptionTiers",
                column: "MaxDimensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSubscriptionTiers_AppDimensions_MaxDimensionId",
                table: "AppSubscriptionTiers",
                column: "MaxDimensionId",
                principalTable: "AppDimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSubscriptionTiers_AppDimensions_MaxDimensionId",
                table: "AppSubscriptionTiers");

            migrationBuilder.DropIndex(
                name: "IX_AppSubscriptionTiers_MaxDimensionId",
                table: "AppSubscriptionTiers");

            migrationBuilder.DropColumn(
                name: "MaxDimensionId",
                table: "AppSubscriptionTiers");
        }
    }
}
