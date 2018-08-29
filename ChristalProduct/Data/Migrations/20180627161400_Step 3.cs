using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChristalProduct.Data.Migrations
{
    public partial class Step3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lubes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddedEffects = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Desensitizing = table.Column<bool>(nullable: false),
                    Flavor = table.Column<string>(nullable: true),
                    Hybrid = table.Column<bool>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SiliconBased = table.Column<bool>(nullable: false),
                    WaterBased = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lubes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Toys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Battery = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Glass = table.Column<bool>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Realistic = table.Column<bool>(nullable: false),
                    Recharable = table.Column<bool>(nullable: false),
                    Silicon = table.Column<bool>(nullable: false),
                    WaterProof = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toys", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lubes");

            migrationBuilder.DropTable(
                name: "Toys");
        }
    }
}
