using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplyGlobalSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PK_positions",
                table: "positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locations",
                table: "locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments_positions",
                table: "departments_positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments_locations",
                table: "departments_locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departments",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "positions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "locations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "departments_positions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "departments_positions",
                newName: "position_id");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "departments_positions",
                newName: "department_id");

            migrationBuilder.RenameIndex(
                name: "IX_departments_positions_PositionId",
                table: "departments_positions",
                newName: "ix_departments_positions_position_id");

            migrationBuilder.RenameIndex(
                name: "IX_departments_positions_DepartmentId_PositionId",
                table: "departments_positions",
                newName: "ix_departments_positions_department_id_position_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "departments_locations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "departments_locations",
                newName: "location_id");

            migrationBuilder.RenameColumn(
                name: "IsPrimary",
                table: "departments_locations",
                newName: "is_primary");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "departments_locations",
                newName: "department_id");

            migrationBuilder.RenameIndex(
                name: "IX_departments_locations_LocationId",
                table: "departments_locations",
                newName: "ix_departments_locations_location_id");

            migrationBuilder.RenameIndex(
                name: "IX_departments_locations_DepartmentId_LocationId",
                table: "departments_locations",
                newName: "ix_departments_locations_department_id_location_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "departments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "departments",
                newName: "parent_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_positions",
                table: "positions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_locations",
                table: "locations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_departments_positions",
                table: "departments_positions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_departments_locations",
                table: "departments_locations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_departments",
                table: "departments",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_departments_locations_departments_department_id",
                table: "departments_locations",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_locations_locations_location_id",
                table: "departments_locations",
                column: "location_id",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_positions_departments_department_id",
                table: "departments_positions",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_departments_positions_positions_position_id",
                table: "departments_positions",
                column: "position_id",
                principalTable: "positions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_departments_locations_departments_department_id",
                table: "departments_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_locations_locations_location_id",
                table: "departments_locations");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_positions_departments_department_id",
                table: "departments_positions");

            migrationBuilder.DropForeignKey(
                name: "fk_departments_positions_positions_position_id",
                table: "departments_positions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_positions",
                table: "positions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_locations",
                table: "locations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departments_positions",
                table: "departments_positions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departments_locations",
                table: "departments_locations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_departments",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "positions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "locations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "departments_positions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "position_id",
                table: "departments_positions",
                newName: "PositionId");

            migrationBuilder.RenameColumn(
                name: "department_id",
                table: "departments_positions",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "ix_departments_positions_position_id",
                table: "departments_positions",
                newName: "IX_departments_positions_PositionId");

            migrationBuilder.RenameIndex(
                name: "ix_departments_positions_department_id_position_id",
                table: "departments_positions",
                newName: "IX_departments_positions_DepartmentId_PositionId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "departments_locations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "location_id",
                table: "departments_locations",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "is_primary",
                table: "departments_locations",
                newName: "IsPrimary");

            migrationBuilder.RenameColumn(
                name: "department_id",
                table: "departments_locations",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "ix_departments_locations_location_id",
                table: "departments_locations",
                newName: "IX_departments_locations_LocationId");

            migrationBuilder.RenameIndex(
                name: "ix_departments_locations_department_id_location_id",
                table: "departments_locations",
                newName: "IX_departments_locations_DepartmentId_LocationId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "departments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "parent_id",
                table: "departments",
                newName: "ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_positions",
                table: "positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_locations",
                table: "locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments_positions",
                table: "departments_positions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments_locations",
                table: "departments_locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departments",
                table: "departments",
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
    }
}
