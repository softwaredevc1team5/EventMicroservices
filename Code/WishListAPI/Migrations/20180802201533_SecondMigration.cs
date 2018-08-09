using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WishListAPI.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishCartItem",
                table: "WishCartItem");

            migrationBuilder.RenameTable(
                name: "WishCartItem",
                newName: "WishCartItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishCartItems",
                table: "WishCartItems",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishCartItems",
                table: "WishCartItems");

            migrationBuilder.RenameTable(
                name: "WishCartItems",
                newName: "WishCartItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishCartItem",
                table: "WishCartItem",
                column: "Id");
        }
    }
}
