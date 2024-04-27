using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectMVC.Migrations
{
    /// <inheritdoc />
    public partial class ProyectMVC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    AddressPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DPI = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    AccountNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Personas__2EC8D2ACF746F68E", x => x.IdPersona);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Personas__A9D10534F8ED91A9",
                table: "Personas",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personas");
        }
    }
}
