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
                name: "ProductGroups",
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
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
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
                name: "ProductsLists",
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
                    table.PrimaryKey("PK_ProductsLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsLists_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductSubGroups",
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
                    table.PrimaryKey("PK_ProductSubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSubGroups_ProductGroups_IdProductGroup",
                        column: x => x.IdProductGroup,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
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
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductGroups_IdProductGroup",
                        column: x => x.IdProductGroup,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductSubGroups_IdProductSubGroup",
                        column: x => x.IdProductSubGroup,
                        principalTable: "ProductSubGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ProductsLists_IdProductsList",
                        column: x => x.IdProductsList,
                        principalTable: "ProductsLists",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductsShared",
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
                    table.PrimaryKey("PK_ProductsShared", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsShared_ProductGroups_IdProductGroup",
                        column: x => x.IdProductGroup,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsShared_ProductSubGroups_IdProductSubGroup",
                        column: x => x.IdProductSubGroup,
                        principalTable: "ProductSubGroups",
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
                    { "37846734-172e-4149-8cec-6f43d1eb3f60", 0, "081bc99b-9a0b-491e-838c-f711423720b9", "jmmatheus23@gmail.com", true, "JoaoDiasUser", false, null, new byte[0], "JMMATHEUS23@GMAIL.COM", "JMMATHEUS23@GMAIL.COM", "AQAAAAIAAYagAAAAEJ1Jtrw5IYuAqLnNmOQVwmrX66PkwhJgBcbrATue7r5QpWdNgrIDqPbTH4PYGHPdDA==", null, false, new byte[0], "065821fd-5a68-4786-bc3c-1f8d1e23e56b", false, "jmmatheus23@gmail.com" },
                    { "38846734-172e-4149-8cec-6f43d1eb3f60", 0, "d020301a-c569-4124-9140-5fa601bc53bd", "joaodiasworking@gmail.com", true, "JoaoDiasAdmin", false, null, new byte[0], "JOAODIASWORKING@GMAIL.COM", "JOAODIASWORKING@GMAIL.COM", "AQAAAAIAAYagAAAAEOrocbvh/SY+pZJBmqE1AjqwPX9M9Mf7KJ8ry5bOOhOk7J3eoUQQIxcfBLCzFSol2A==", null, false, new byte[0], "d7d6676a-bd24-47a7-a38d-04248da9c5e8", false, "joaodiasworking@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "Image", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6366), "GERAL", true, new byte[] { 0 }, "GERAL", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6378) });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "37846734-172e-4149-8cec-6f43d1eb3f60" },
                    { "1", "38846734-172e-4149-8cec-6f43d1eb3f60" }
                });

            migrationBuilder.InsertData(
                table: "ProductSubGroups",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "Image", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6531), "GERAL", true, 1, new byte[] { 0 }, "GERAL", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6532) });

            migrationBuilder.InsertData(
                table: "ProductsLists",
                columns: new[] { "Id", "CreatedDate", "Description", "ExternalLink", "IdUser", "Image", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6560), "Description for Shopping List 1", "", "37846734-172e-4149-8cec-6f43d1eb3f60", new byte[] { 0 }, "Shopping List 1", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6560) },
                    { 2, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6563), "Description for Shopping List 2", "", "37846734-172e-4149-8cec-6f43d1eb3f60", new byte[] { 0 }, "Shopping List 2", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6563) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6635), "TESTE 1", true, 1, 1, 1, new byte[] { 0 }, "", "Teste 1", new byte[] { 0 }, "Meter", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6636), 22.05m },
                    { 2, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6639), "TESTE 2", true, 1, 1, 1, new byte[] { 0 }, "", "Teste 2", new byte[] { 0 }, "SquareMeter", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6639), 33.33m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 3, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6642), "TESTE 3", 1, 1, 1, new byte[] { 0 }, "", "Teste 3", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6642), 42.33m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6645), "TESTE 4", true, 1, 1, 2, new byte[] { 0 }, "", "Teste 4", new byte[] { 0 }, "CubicMeter", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6645), 77.77m },
                    { 5, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6646), "TESTE 5", true, 1, 1, 2, new byte[] { 0 }, "", "Teste 5", new byte[] { 0 }, "Unit", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6647), 66.66m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 6, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6648), "TESTE 6", 1, 1, 2, new byte[] { 0 }, "", "Teste 6", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6649), 35.31m });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6585), "TESTE 1", true, 1, 1, new byte[] { 0 }, "", "Teste 1", new byte[] { 0 }, "Meter", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6585), 22.05m },
                    { 2, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6589), "TESTE 2", true, 1, 1, new byte[] { 0 }, "", "Teste 2", new byte[] { 0 }, "SquareMeter", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6590), 33.33m }
                });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 3, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6592), "TESTE 3", 1, 1, new byte[] { 0 }, "", "Teste 3", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6592), 42.33m });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6593), "TESTE 4", true, 1, 1, new byte[] { 0 }, "", "Teste 4", new byte[] { 0 }, "CubicMeter", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6594), 77.77m },
                    { 5, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6595), "TESTE 5", true, 1, 1, new byte[] { 0 }, "", "Teste 5", new byte[] { 0 }, "Unit", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6596), 66.66m }
                });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 6, new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6597), "TESTE 6", 1, 1, new byte[] { 0 }, "", "Teste 6", new byte[] { 0 }, "Unspecified", new DateTime(2024, 6, 1, 14, 6, 56, 278, DateTimeKind.Local).AddTicks(6597), 35.31m });

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
                name: "IX_ProductGroups_Name",
                table: "ProductGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdProductGroup",
                table: "Products",
                column: "IdProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdProductsList",
                table: "Products",
                column: "IdProductsList");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IdProductSubGroup",
                table: "Products",
                column: "IdProductSubGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsLists_IdUser",
                table: "ProductsLists",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsLists_Name",
                table: "ProductsLists",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsShared_IdProductGroup",
                table: "ProductsShared",
                column: "IdProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsShared_IdProductSubGroup",
                table: "ProductsShared",
                column: "IdProductSubGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsShared_Name",
                table: "ProductsShared",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubGroups_IdProductGroup",
                table: "ProductSubGroups",
                column: "IdProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubGroups_Name",
                table: "ProductSubGroups",
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
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductsShared");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ProductsLists");

            migrationBuilder.DropTable(
                name: "ProductSubGroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProductGroups");
        }
    }
}
