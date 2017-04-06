using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HierarchicalTree.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Organization_OrganizationId",
                table: "Countries");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Organization_OrganizationId",
                table: "Countries",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Organization_OrganizationId",
                table: "Countries");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizationId",
                table: "Countries",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Organization_OrganizationId",
                table: "Countries",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
