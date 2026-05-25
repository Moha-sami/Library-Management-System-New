using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library_mangamnet_system.Migrations
{
    /// <inheritdoc />
    public partial class FixMembershipDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Publicationyear",
                table: "Books",
                newName: "PublicationYear");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MembershipDate",
                table: "Members",
                type: "date",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldDefaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicationYear",
                table: "Books",
                newName: "Publicationyear");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MembershipDate",
                table: "Members",
                type: "date",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
