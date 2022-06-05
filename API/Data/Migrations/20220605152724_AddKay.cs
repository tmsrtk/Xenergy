using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class AddKay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGroupAccessRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AccessRuleId = table.Column<int>(type: "INTEGER", nullable: true),
                    UserGroupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupAccessRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroupAccessRule_AccessRules_AccessRuleId",
                        column: x => x.AccessRuleId,
                        principalTable: "AccessRules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserGroupAccessRule_UserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UserGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupAccessRule_AccessRuleId",
                table: "UserGroupAccessRule",
                column: "AccessRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupAccessRule_UserGroupId",
                table: "UserGroupAccessRule",
                column: "UserGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGroupAccessRule");
        }
    }
}
