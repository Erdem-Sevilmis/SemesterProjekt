using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourPlanner.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreColumnsToTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Tours",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Tours",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Tours",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "Tours",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransportType",
                table: "Tours",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TransportType",
                table: "Tours");
        }
    }
}
