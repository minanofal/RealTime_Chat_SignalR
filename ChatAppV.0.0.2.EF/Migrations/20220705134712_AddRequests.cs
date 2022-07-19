using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAppV._0._0._2.EF.Migrations
{
    public partial class AddRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendRequists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResiverId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequists_Users_ResiverId",
                        column: x => x.ResiverId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendRequists_Users_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequists_ResiverId",
                table: "FriendRequists",
                column: "ResiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequists_SenderId",
                table: "FriendRequists",
                column: "SenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendRequists");
        }
    }
}
