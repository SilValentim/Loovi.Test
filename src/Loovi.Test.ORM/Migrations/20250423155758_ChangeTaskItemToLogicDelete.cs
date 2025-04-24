using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loovi.Test.ORM.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTaskItemToLogicDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Tasks",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Tasks",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Tasks",
                newName: "CreationDate");
        }
    }
}
