using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc1547cd-0e64-461b-91b0-bb20c8552eea", "AQAAAAIAAYagAAAAEFCFG2mOl5O32RqA8GhJ6WHKDx4nWmR+0OAz3Q9PNgMxOxLVkyITb29LvcHaNEH2kA==", "91e42490-ffa0-49b3-969f-b53b380f43eb" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1824ab7c-2a54-4e9f-8d63-3074c84e075b", "AQAAAAIAAYagAAAAEDNkJREbh/Ct0a2VEafhzz07uhodpAHMHZ7Y+SMWWxOrnF0nmEuiD8bPUXr/jQ1XzA==", "e9b71a05-2350-4f8c-882d-447a170d5a38" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "146ffb71-b66f-4ce0-acec-13c64b16317f", "AQAAAAIAAYagAAAAENfVHPt3uIi5VOmviSGoMlMP7bTUULMFboiEx99Ltwtj102mB/CEEVMqw2ONl5+0qQ==", "a47fcc65-7b1f-4a82-b6ee-e54b4d513a3b" });
        }
    }
}
