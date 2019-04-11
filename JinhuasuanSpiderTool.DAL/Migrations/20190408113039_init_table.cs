using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JinhuasuanSpiderTool.DAL.Migrations
{
    public partial class init_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deyouyun_address_city",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdentificationCode = table.Column<string>(maxLength: 10, nullable: true),
                    ProvinceId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 6, nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Initial = table.Column<string>(nullable: false),
                    SimpleSpell = table.Column<string>(maxLength: 50, nullable: true),
                    FullSpell = table.Column<string>(maxLength: 200, nullable: true),
                    Unique = table.Column<string>(maxLength: 6, nullable: true),
                    KeyWordJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deyouyun_address_city", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "deyouyun_address_districts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdentificationCode = table.Column<string>(maxLength: 10, nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Initial = table.Column<string>(nullable: false),
                    SimpleSpell = table.Column<string>(maxLength: 50, nullable: true),
                    FullSpell = table.Column<string>(maxLength: 200, nullable: true),
                    Unique = table.Column<string>(maxLength: 6, nullable: true),
                    KeyWordJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deyouyun_address_districts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "deyouyun_address_province",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdentificationCode = table.Column<string>(maxLength: 10, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Initial = table.Column<string>(nullable: false),
                    SimpleSpell = table.Column<string>(maxLength: 50, nullable: true),
                    FullSpell = table.Column<string>(maxLength: 200, nullable: true),
                    Unique = table.Column<string>(maxLength: 6, nullable: true),
                    KeyWordJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deyouyun_address_province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "jinhuasuan_store",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    store_id = table.Column<int>(nullable: false),
                    create_time = table.Column<DateTime>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    sync_time = table.Column<DateTime>(nullable: true),
                    error_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jinhuasuan_store", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deyouyun_address_city");

            migrationBuilder.DropTable(
                name: "deyouyun_address_districts");

            migrationBuilder.DropTable(
                name: "deyouyun_address_province");

            migrationBuilder.DropTable(
                name: "jinhuasuan_store");
        }
    }
}
