using Microsoft.EntityFrameworkCore.Migrations;

namespace JinhuasuanSpiderTool.DAL.Migrations
{
    public partial class add_province_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
