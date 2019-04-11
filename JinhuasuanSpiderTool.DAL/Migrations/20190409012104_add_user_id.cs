using Microsoft.EntityFrameworkCore.Migrations;

namespace JinhuasuanSpiderTool.DAL.Migrations
{
    public partial class add_user_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "own_store_id",
                table: "jinhuasuan_store",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "own_user_id",
                table: "jinhuasuan_store",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "own_store_id",
                table: "jinhuasuan_store");

            migrationBuilder.DropColumn(
                name: "own_user_id",
                table: "jinhuasuan_store");
        }
    }
}
