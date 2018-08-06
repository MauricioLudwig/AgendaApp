using Microsoft.EntityFrameworkCore.Migrations;

namespace AgendaApp.Migrations
{
    public partial class optional_agenda_title : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Agendas",
                maxLength: 256,
                nullable: true,
                defaultValue: "Agenda",
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Agendas",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true,
                oldDefaultValue: "Agenda");
        }
    }
}
