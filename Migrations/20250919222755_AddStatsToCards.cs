using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardCollector.Migrations
{
    /// <inheritdoc />
    public partial class AddStatsToCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Cards",
                newName: "BaseSpeed");

            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Cards",
                newName: "BaseLevel");

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "UserCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomAvatarUrl",
                table: "UserCards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Defense",
                table: "UserCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "UserCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HitPoints",
                table: "UserCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "UserCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "UserCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseAttack",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseDefense",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseExperience",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseHitPoints",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DefaultImageUrl",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "BaseAttack", "BaseDefense", "BaseExperience", "BaseHitPoints", "BaseLevel", "DefaultImageUrl" },
                values: new object[] { 1, 1, 0, 10, 1, null });

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "BaseAttack", "BaseDefense", "BaseExperience", "BaseHitPoints", "BaseLevel", "DefaultImageUrl" },
                values: new object[] { 1, 1, 0, 10, 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_CardId",
                table: "UserCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCards_UserId",
                table: "UserCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_Cards_CardId",
                table: "UserCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCards_Users_UserId",
                table: "UserCards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_Cards_CardId",
                table: "UserCards");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCards_Users_UserId",
                table: "UserCards");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_UserCards_CardId",
                table: "UserCards");

            migrationBuilder.DropIndex(
                name: "IX_UserCards_UserId",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "CustomAvatarUrl",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "Defense",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "HitPoints",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "UserCards");

            migrationBuilder.DropColumn(
                name: "BaseAttack",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BaseDefense",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BaseExperience",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BaseHitPoints",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "DefaultImageUrl",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "BaseSpeed",
                table: "Cards",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "BaseLevel",
                table: "Cards",
                newName: "Experience");

            migrationBuilder.AddColumn<Guid>(
                name: "AccessToken",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Experience",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Cards",
                keyColumn: "CardId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Experience",
                value: 0);
        }
    }
}
