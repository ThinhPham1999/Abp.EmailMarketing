using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Abp.EmailMarketing.Migrations
{
    public partial class add_lazy_loading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppGroupCampaign");

            migrationBuilder.CreateTable(
                name: "CampaignGroup",
                columns: table => new
                {
                    CampaignsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignGroup", x => new { x.CampaignsId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_CampaignGroup_AppCampaign_CampaignsId",
                        column: x => x.CampaignsId,
                        principalTable: "AppCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignGroup_AppGroup_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "AppGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignGroup_GroupsId",
                table: "CampaignGroup",
                column: "GroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignGroup");

            migrationBuilder.CreateTable(
                name: "AppGroupCampaign",
                columns: table => new
                {
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGroupCampaign", x => new { x.CampaignId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_AppGroupCampaign_AppCampaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "AppCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppGroupCampaign_AppGroup_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AppGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppGroupCampaign_GroupId",
                table: "AppGroupCampaign",
                column: "GroupId");
        }
    }
}
