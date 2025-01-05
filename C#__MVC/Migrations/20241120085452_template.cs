using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace C___MVC.Migrations
{
    /// <inheritdoc />
    public partial class template : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HtmlContent",
                table: "CvTemplate",
                newName: "HtmlEditContent");

            migrationBuilder.AddColumn<string>(
                name: "HtmlCreateContent",
                table: "CvTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "CV",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HtmlCreateContent",
                table: "CvTemplate");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "CV");

            migrationBuilder.RenameColumn(
                name: "HtmlEditContent",
                table: "CvTemplate",
                newName: "HtmlContent");
        }
    }
}
