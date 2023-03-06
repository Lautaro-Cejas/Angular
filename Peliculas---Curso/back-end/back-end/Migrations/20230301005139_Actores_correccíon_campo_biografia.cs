using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class Actores_correccíon_campo_biografia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Biogriafia",
                table: "Actores",
                newName: "Biografia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Biografia",
                table: "Actores",
                newName: "Biogriafia");
        }
    }
}
