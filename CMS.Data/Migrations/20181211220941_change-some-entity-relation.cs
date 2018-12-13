using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Data.Migrations
{
    public partial class changesomeentityrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Address_AddressId",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Contact_ContactId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ContactId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Contact_AddressId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Contact");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Contact",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Address",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CustomerId",
                table: "Contact",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Customer_CustomerId",
                table: "Address",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Customer_CustomerId",
                table: "Contact",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Customer_CustomerId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Customer_CustomerId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CustomerId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Address_CustomerId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Address");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ContactId",
                table: "Customer",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_AddressId",
                table: "Contact",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Address_AddressId",
                table: "Contact",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Contact_ContactId",
                table: "Customer",
                column: "ContactId",
                principalTable: "Contact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
