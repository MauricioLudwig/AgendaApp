using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaApp.Migrations
{
    public partial class archiveddateadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedDate",
                table: "Agendas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivedDate",
                table: "Agendas");
        }
    }
}
