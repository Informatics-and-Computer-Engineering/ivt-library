using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Library.Migrations
{
    public partial class TypetoFileTypeInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameTable(
                name: "type",
                newName: "file_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameTable(
                name: "file_type",
                newName: "type");
        }
    }
}
