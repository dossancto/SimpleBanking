using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleBanking.Infra.Migrations
{
    /// <inheritdoc />
    public partial class createdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CNPJ = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Debit = table.Column<int>(type: "integer", nullable: false),
                    DebitFactor = table.Column<int>(type: "integer", nullable: false),
                    ResponsableFullName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    HashedPassword = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Search = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                    table.CheckConstraint("CK_MERCHANT_Balance", "\"Debit\" >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CPF = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Debit = table.Column<int>(type: "integer", nullable: false),
                    DebitFactor = table.Column<int>(type: "integer", nullable: false),
                    ResponsableFullName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    HashedPassword = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Search = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.CheckConstraint("CK_Users_Balance", "\"Debit\" >= 0");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_CNPJ",
                table: "Merchants",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_EmailAddress",
                table: "Merchants",
                column: "EmailAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CPF",
                table: "Persons",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_EmailAddress",
                table: "Persons",
                column: "EmailAddress",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
