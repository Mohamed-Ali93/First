using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First.Migrations
{
    /// <inheritdoc />
    public partial class AddExtendedUserFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoginAttemptCount",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserStatus",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LoginAttemptCount",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "AbpUsers");
        }
    }
}
