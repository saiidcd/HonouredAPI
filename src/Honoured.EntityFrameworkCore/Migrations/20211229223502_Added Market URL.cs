using Microsoft.EntityFrameworkCore.Migrations;

namespace Honoured.Migrations
{
    public partial class AddedMarketURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppArtists_AppPersons_PersonalDetailsId",
                table: "AppArtists");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPlacements_AppSubscriptions_SubscriptionId",
                table: "AppPlacements");

            migrationBuilder.DropIndex(
                name: "IX_AppArtists_PersonalDetailsId",
                table: "AppArtists");

            migrationBuilder.DropColumn(
                name: "PersonalDetailsId",
                table: "AppArtists");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "AppPlacements",
                newName: "ArtLoverSubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_AppPlacements_SubscriptionId",
                table: "AppPlacements",
                newName: "IX_AppPlacements_ArtLoverSubscriptionId");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "AppMarkets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppPersons_ParentId",
                table: "AppPersons",
                column: "ParentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPersons_AppArtists_ParentId",
                table: "AppPersons",
                column: "ParentId",
                principalTable: "AppArtists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPlacements_AppSubscriptions_ArtLoverSubscriptionId",
                table: "AppPlacements",
                column: "ArtLoverSubscriptionId",
                principalTable: "AppSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPersons_AppArtists_ParentId",
                table: "AppPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPlacements_AppSubscriptions_ArtLoverSubscriptionId",
                table: "AppPlacements");

            migrationBuilder.DropIndex(
                name: "IX_AppPersons_ParentId",
                table: "AppPersons");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "AppMarkets");

            migrationBuilder.RenameColumn(
                name: "ArtLoverSubscriptionId",
                table: "AppPlacements",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_AppPlacements_ArtLoverSubscriptionId",
                table: "AppPlacements",
                newName: "IX_AppPlacements_SubscriptionId");

            migrationBuilder.AddColumn<long>(
                name: "PersonalDetailsId",
                table: "AppArtists",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppArtists_PersonalDetailsId",
                table: "AppArtists",
                column: "PersonalDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppArtists_AppPersons_PersonalDetailsId",
                table: "AppArtists",
                column: "PersonalDetailsId",
                principalTable: "AppPersons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPlacements_AppSubscriptions_SubscriptionId",
                table: "AppPlacements",
                column: "SubscriptionId",
                principalTable: "AppSubscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
