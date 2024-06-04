using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ListGenius.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LogoImage = table.Column<byte[]>(type: "longblob", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "longblob", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_groups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products_lists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Public = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    ExternalLink = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdUser = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_lists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_lists_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product_sub_groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    IdProductGroup = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_sub_groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_sub_groups_product_groups_IdProductGroup",
                        column: x => x.IdProductGroup,
                        principalTable: "product_groups",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProductsList = table.Column<int>(type: "int", nullable: false),
                    IdProductGroup = table.Column<int>(type: "int", nullable: false),
                    IdProductSubGroup = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Qrcode = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Link = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_product_groups_IdProductGroup",
                        column: x => x.IdProductGroup,
                        principalTable: "product_groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_products_product_sub_groups_IdProductSubGroup",
                        column: x => x.IdProductSubGroup,
                        principalTable: "product_sub_groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_products_products_lists_IdProductsList",
                        column: x => x.IdProductsList,
                        principalTable: "products_lists",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products_shared",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdProductGroup = table.Column<int>(type: "int", nullable: false),
                    IdProductSubGroup = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Qrcode = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Link = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_shared", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_shared_product_groups_IdProductGroup",
                        column: x => x.IdProductGroup,
                        principalTable: "product_groups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_products_shared_product_sub_groups_IdProductSubGroup",
                        column: x => x.IdProductSubGroup,
                        principalTable: "product_sub_groups",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "LogoImage", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "37846734-172e-4149-8cec-6f43d1eb3f60", 0, "ddcce1c5-dedc-4b15-bd0d-2459eae1eca3", "jmmatheus23@gmail.com", true, "JoaoDiasUser", false, null, new byte[0], "JMMATHEUS23@GMAIL.COM", "JMMATHEUS23@GMAIL.COM", "AQAAAAIAAYagAAAAELyEP47grXttIRiAMMJQ+1VhMK7etybhEwlVEXNxiY72zvE1MFZdDCVj8+EPS2iIEA==", null, false, new byte[0], "019fb84c-b01c-47dd-8e24-cba7bde88427", false, "jmmatheus23@gmail.com" },
                    { "38846734-172e-4149-8cec-6f43d1eb3f60", 0, "35668527-62b2-4c59-8b7c-44e5e16143a8", "joaodiasworking@gmail.com", true, "JoaoDiasAdmin", false, null, new byte[0], "JOAODIASWORKING@GMAIL.COM", "JOAODIASWORKING@GMAIL.COM", "AQAAAAIAAYagAAAAEIii4mVINuVD04Umus7e/reoOzNZNy0404aq55WvWxIU+gtXCbM0JXz5xHjd1DAQJw==", null, false, new byte[0], "7163a134-2c7d-4bdd-ac47-b81aeafc8948", false, "joaodiasworking@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "product_groups",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "Image", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7504), "GERAL", true, new byte[] { 0 }, "GERAL", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7516) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "37846734-172e-4149-8cec-6f43d1eb3f60" },
                    { "1", "38846734-172e-4149-8cec-6f43d1eb3f60" }
                });

            migrationBuilder.InsertData(
                table: "product_sub_groups",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "Image", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7563), "GERAL", true, 1, new byte[] { 0 }, "GERAL", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7563) });

            migrationBuilder.InsertData(
                table: "products_lists",
                columns: new[] { "Id", "CreatedDate", "Description", "ExternalLink", "IdUser", "Image", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7589), "Description for Shopping List 1", "", "37846734-172e-4149-8cec-6f43d1eb3f60", new byte[] { 0 }, "Shopping List 1", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7590) },
                    { 2, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7591), "Description for Shopping List 2", "", "37846734-172e-4149-8cec-6f43d1eb3f60", new byte[] { 0 }, "Shopping List 2", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7591) }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7740), "TESTE 1", true, 1, 1, 1, new byte[] { 0 }, "", "Teste 1", new byte[] { 0 }, "Meter", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7741), 22.05m },
                    { 2, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7743), "TESTE 2", true, 1, 1, 1, new byte[] { 0 }, "", "Teste 2", new byte[] { 0 }, "SquareMeter", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7743), 33.33m }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 3, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7745), "TESTE 3", 1, 1, 1, new byte[] { 0 }, "", "Teste 3", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7745), 42.33m });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7746), "TESTE 4", true, 1, 1, 2, new byte[] { 0 }, "", "Teste 4", new byte[] { 0 }, "CubicMeter", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7746), 77.77m },
                    { 5, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7747), "TESTE 5", true, 1, 1, 2, new byte[] { 0 }, "", "Teste 5", new byte[] { 0 }, "Unit", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7748), 66.66m }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 6, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7749), "TESTE 6", 1, 1, 2, new byte[] { 0 }, "", "Teste 6", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7749), 35.31m });

            migrationBuilder.InsertData(
                table: "products_shared",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7612), "TESTE 1", true, 1, 1, new byte[] { 0 }, "", "Teste 1", new byte[] { 0 }, "Meter", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7613), 22.05m },
                    { 2, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7618), "TESTE 2", true, 1, 1, new byte[] { 0 }, "", "Teste 2", new byte[] { 0 }, "SquareMeter", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7619), 33.33m }
                });

            migrationBuilder.InsertData(
                table: "products_shared",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 3, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7620), "TESTE 3", 1, 1, new byte[] { 0 }, "", "Teste 3", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7620), 42.33m });

            migrationBuilder.InsertData(
                table: "products_shared",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7621), "TESTE 4", true, 1, 1, new byte[] { 0 }, "", "Teste 4", new byte[] { 0 }, "CubicMeter", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7622), 77.77m },
                    { 5, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7697), "TESTE 5", true, 1, 1, new byte[] { 0 }, "", "Teste 5", new byte[] { 0 }, "Unit", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7698), 66.66m }
                });

            migrationBuilder.InsertData(
                table: "products_shared",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 6, new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7699), "TESTE 6", 1, 1, new byte[] { 0 }, "", "Teste 6", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 4, 18, 32, 46, 127, DateTimeKind.Local).AddTicks(7700), 35.31m });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_groups_Name",
                table: "product_groups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_sub_groups_IdProductGroup",
                table: "product_sub_groups",
                column: "IdProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_product_sub_groups_Name",
                table: "product_sub_groups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_IdProductGroup",
                table: "products",
                column: "IdProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_products_IdProductsList",
                table: "products",
                column: "IdProductsList");

            migrationBuilder.CreateIndex(
                name: "IX_products_IdProductSubGroup",
                table: "products",
                column: "IdProductSubGroup");

            migrationBuilder.CreateIndex(
                name: "IX_products_Name",
                table: "products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_lists_IdUser",
                table: "products_lists",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_products_lists_Name",
                table: "products_lists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_shared_IdProductGroup",
                table: "products_shared",
                column: "IdProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_products_shared_IdProductSubGroup",
                table: "products_shared",
                column: "IdProductSubGroup");

            migrationBuilder.CreateIndex(
                name: "IX_products_shared_Name",
                table: "products_shared",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "products_shared");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "products_lists");

            migrationBuilder.DropTable(
                name: "product_sub_groups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "product_groups");
        }
    }
}
