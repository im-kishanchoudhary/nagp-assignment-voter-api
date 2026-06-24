using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VoterApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "voters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_number = table.Column<string>(type: "text", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    has_voted = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voters", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "voters",
                columns: new[] { "id", "address", "age", "created_at", "full_name", "gender", "has_voted", "id_number", "updated_at" },
                values: new object[,]
                {
                    { 1, "North Ward", 34, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Asha Rao", "Female", false, "IDN0000001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "East Ward", 45, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vikram Singh", "Male", true, "IDN0000002", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "South Ward", 29, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Meera Nair", "Female", false, "IDN0000003", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "West Ward", 52, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Rahul Verma", "Male", true, "IDN0000004", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "Central Ward", 38, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Sana Khan", "Female", false, "IDN0000005", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "North Ward", 41, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Arjun Mehta", "Male", true, "IDN0000006", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "East Ward", 27, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Priya Iyer", "Female", false, "IDN0000007", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_voters_id_number",
                table: "voters",
                column: "id_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voters");
        }
    }
}
