using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.EmailMarketing.Migrations
{
    public partial class Added_GroupId_To_Contact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
