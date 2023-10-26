using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notepad.Storage.Migrations
{
    public partial class SrtNllable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Chunks_ChunkId",
                table: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChunkId",
                table: "Notes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Chunks_ChunkId",
                table: "Notes",
                column: "ChunkId",
                principalTable: "Chunks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Chunks_ChunkId",
                table: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChunkId",
                table: "Notes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Chunks_ChunkId",
                table: "Notes",
                column: "ChunkId",
                principalTable: "Chunks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
