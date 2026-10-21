using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLocations_Departments_DepartmentId",
                table: "DepartmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentLocations_Locations_LocationId",
                table: "DepartmentLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPositions_Departments_DepartmentId",
                table: "DepartmentPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentPositions_positions_PositionId",
                table: "DepartmentPositions");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_ParentId",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ParentId",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentPositions",
                table: "DepartmentPositions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DepartmentLocations",
                table: "DepartmentLocations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "locations");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "departments");

            migrationBuilder.RenameTable(
                name: "DepartmentPositions",
                newName: "departments_positions");

            migrationBuilder.RenameTable(
                name: "DepartmentLocations",
                newName: "departments_locations");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "departments",
                newName: "slug");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentPositions_PositionId",
                table: "departments_positions",
                newName: "IX_departments_positions_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentPositions_DepartmentId_PositionId",
                table: "departments_positions",
                newName: "IX_departments_positions_DepartmentId_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentLocations_LocationId",
                table: "departments_locations",
                newName: "IX_departments_locations_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_DepartmentLocations_DepartmentId_LocationId",
                table: "departments_locations",
                newName: "IX_departments_locations_DepartmentId_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_locations",
                table: "locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments_positions",
                table: "departments_positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments_locations",
                table: "departments_locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_locations_departments_DepartmentId",
                table: "departments_locations",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_locations_locations_LocationId",
                table: "departments_locations",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_positions_departments_DepartmentId",
                table: "departments_positions",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_departments_positions_positions_PositionId",
                table: "departments_positions",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_locations_departments_DepartmentId",
                table: "departments_locations");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_locations_locations_LocationId",
                table: "departments_locations");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_positions_departments_DepartmentId",
                table: "departments_positions");

            migrationBuilder.DropForeignKey(
                name: "FK_departments_positions_positions_PositionId",
                table: "departments_positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locations",
                table: "locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments_positions",
                table: "departments_positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments_locations",
                table: "departments_locations");

            migrationBuilder.RenameTable(
                name: "locations",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "departments",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "departments_positions",
                newName: "DepartmentPositions");

            migrationBuilder.RenameTable(
                name: "departments_locations",
                newName: "DepartmentLocations");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "Departments",
                newName: "Slug");

            migrationBuilder.RenameIndex(
                name: "IX_departments_positions_PositionId",
                table: "DepartmentPositions",
                newName: "IX_DepartmentPositions_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_positions_DepartmentId_PositionId",
                table: "DepartmentPositions",
                newName: "IX_DepartmentPositions_DepartmentId_PositionId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_locations_LocationId",
                table: "DepartmentLocations",
                newName: "IX_DepartmentLocations_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_departments_locations_DepartmentId_LocationId",
                table: "DepartmentLocations",
                newName: "IX_DepartmentLocations_DepartmentId_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentPositions",
                table: "DepartmentPositions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DepartmentLocations",
                table: "DepartmentLocations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentId",
                table: "Departments",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLocations_Departments_DepartmentId",
                table: "DepartmentLocations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentLocations_Locations_LocationId",
                table: "DepartmentLocations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPositions_Departments_DepartmentId",
                table: "DepartmentPositions",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentPositions_positions_PositionId",
                table: "DepartmentPositions",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_ParentId",
                table: "Departments",
                column: "ParentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
