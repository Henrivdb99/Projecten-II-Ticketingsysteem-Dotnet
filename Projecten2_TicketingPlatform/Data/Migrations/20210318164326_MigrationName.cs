using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projecten2_TicketingPlatform.Data.Migrations
{
    public partial class MigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KnowledgeBase",
                columns: table => new
                {
                    KnowledgeBaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(nullable: true),
                    DatumToevoegen = table.Column<DateTime>(nullable: false),
                    Omschrijving = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBase", x => x.KnowledgeBaseId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnowledgeBase");
        }
    }
}
