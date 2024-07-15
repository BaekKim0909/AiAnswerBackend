using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiAnswerBackend.Migrations
{
    /// <inheritdoc />
    public partial class updateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 13, 15, 4, 41, 913, DateTimeKind.Local).AddTicks(9570),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 13, 15, 3, 10, 801, DateTimeKind.Local).AddTicks(6240));

            migrationBuilder.AlterColumn<byte>(
                name: "IsDelete",
                table: "user",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 13, 15, 4, 41, 913, DateTimeKind.Local).AddTicks(9170),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 13, 15, 3, 10, 801, DateTimeKind.Local).AddTicks(5440));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdateTime",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 13, 15, 3, 10, 801, DateTimeKind.Local).AddTicks(6240),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 13, 15, 4, 41, 913, DateTimeKind.Local).AddTicks(9570));

            migrationBuilder.AlterColumn<byte>(
                name: "IsDelete",
                table: "user",
                type: "tinyint unsigned",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned",
                oldDefaultValue: (byte)0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateTime",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 13, 15, 3, 10, 801, DateTimeKind.Local).AddTicks(5440),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 13, 15, 4, 41, 913, DateTimeKind.Local).AddTicks(9170));
        }
    }
}
