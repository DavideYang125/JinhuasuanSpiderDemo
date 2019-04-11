using Microsoft.EntityFrameworkCore.Migrations;

namespace JinhuasuanSpiderTool.DAL.Migrations
{
    public partial class add_img_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                table: "jinhuasuan_store");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "jinhuasuan_store");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "jinhuasuan_store");

            migrationBuilder.AddColumn<bool>(
                name: "img_replaced",
                table: "jinhuasuan_store",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_replaced",
                table: "jinhuasuan_store");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "jinhuasuan_store",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "jinhuasuan_store",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "jinhuasuan_store",
                nullable: true);
        }
    }
}
