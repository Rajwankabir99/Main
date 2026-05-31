using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Infrastructure.Data.Migrations.Identity
{
    /// <inheritdoc />
    public partial class InitialIdentityMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0d4-00fd-090a-ca10-0d00a1118ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f01570f9-308b-4206-81a4-227bcc80256c", "AQAAAAIAAYagAAAAEKxkenZHweHdQMR6B6BlC5uEtU2XL9mvlPHhJIbJMnTIlpgLRpxRXSYkUce2wjqQPg==", "3c655ec3-e2e0-478a-913d-6e94c8962770" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62a7b545-2c49-450b-9672-19bd35686c98", "AQAAAAIAAYagAAAAEG8mb7l+SJHfjUacFbGx9rSRjOVLv9ScHQA+bPpYTs6DQDg40tRWAp2SKfTrgOv83g==", "a8da0306-3826-406e-9f1a-02341cf9c696" });

            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "e03fd0e4-00fd-090a-ca10-0d00a0018ba5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4743135f-1eca-4176-8a5c-66e599f54a7a", "AQAAAAIAAYagAAAAECBNs6UwuCmxL2glWDmjx4YlmLmIxOzD92DNnf/ja8nv4K9ncUgVJd2zoKcXX2Kg8w==", "7c73f5f4-4716-4122-8403-1883b3a0cd11" });
        }
    }
}
