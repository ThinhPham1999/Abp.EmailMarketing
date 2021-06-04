using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.EmailMarketing.Migrations
{
    public partial class Contact_Group_Many_to_Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppContact_AppGroup_GroupId",
                table: "AppContact");

            migrationBuilder.DropIndex(
                name: "IX_AppContact_GroupId",
                table: "AppContact");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AppContact");

            migrationBuilder.CreateTable(
                name: "ContactGroup",
                columns: table => new
                {
                    ContactsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactGroup", x => new { x.ContactsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_ContactGroup_AppContact_ContactsId",
                        column: x => x.ContactsId,
                        principalTable: "AppContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactGroup_AppGroup_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "AppGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactGroup_GroupsId",
                table: "ContactGroup",
                column: "GroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactGroup");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "AppContact",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppContact_GroupId",
                table: "AppContact",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppContact_AppGroup_GroupId",
                table: "AppContact",
                column: "GroupId",
                principalTable: "AppGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
