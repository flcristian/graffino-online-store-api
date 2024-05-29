using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace graffino_online_store_api.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFKProductPropertyToPropertiesOnDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperties_Properties_propertyId",
                table: "ProductProperties");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_Properties_propertyId",
                table: "ProductProperties",
                column: "propertyId",
                principalTable: "Properties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProperties_Properties_propertyId",
                table: "ProductProperties");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProperties_Properties_propertyId",
                table: "ProductProperties",
                column: "propertyId",
                principalTable: "Properties",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
