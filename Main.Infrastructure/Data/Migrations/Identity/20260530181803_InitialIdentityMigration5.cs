using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0563cadb-79db-4fb0-a710-99ee112f2b98", "AQAAAAIAAYagAAAAELZtUKZY6Sq7iiQ83fqFGgaoLzrFk2ejRZJlKmYdlACeMFrlJvUERtUuP74KBI3UtQ==", "8aa93355-2ff5-4bc7-8729-e10236cfd3cd" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bffcaf2d-8157-43bf-bf22-38e571fe7abf", "AQAAAAIAAYagAAAAEIJghpDCpu4X8sOylNT/RPF+1U17Wc3blL6YKiED2+XykNDIzAhGtdpesdpIccttog==", "38a28868-25e4-4418-9819-01be5c67c90c" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "413edaba-0cde-49ed-ab0e-31258e83972c", "AQAAAAIAAYagAAAAEP26Nr452g0A5zXrAuowhkpa4YKMy6GmdAgE5MCTcf0opK4HOgGmovLcBxbUvGgeJw==", "ff8bad33-cbbf-46a5-ac83-fac0c7721e9c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9e76159-abb1-4c3a-891d-bf681f39a923", "AQAAAAIAAYagAAAAELNg7TfoaV2CPNGW+AwcUyFYzTQEtZ4ycIvZSORV+59Wwdac9gDqfceBVJdwRXwZVQ==", "bb638d16-3c86-41fb-9e77-3a437f85c71c" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "514c602f-11ff-4f47-8e16-8627e51e32dd", "AQAAAAIAAYagAAAAEBW4lXrZDSUDDGYAAKxnOHHNZzUjjj3xtpwbxgENP7gO/M6gf4i8w22/ugXMzgH7Lw==", "4c1f41b8-5e55-4888-92ad-4ca6959b5df0" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d18992e6-179f-4f97-9e97-4b17b0dde11a", "AQAAAAIAAYagAAAAEJm+rryz0NJC4jVJFZYS32/OmhvVyDUTGa1u2G7X6f4Tsf+Gqkj9rR6d00bC+BMYHw==", "a2e04533-535b-4eeb-95f7-6da624e916d2" });
        }
    }
}
