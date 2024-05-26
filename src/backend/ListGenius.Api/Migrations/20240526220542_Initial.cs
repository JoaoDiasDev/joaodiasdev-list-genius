using System;
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
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Public = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExternalLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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
                });

            migrationBuilder.CreateTable(
                name: "ProductSubGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    IdProductGroup = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProductsList = table.Column<int>(type: "int", nullable: false),
                    IdProductGroup = table.Column<int>(type: "int", nullable: false),
                    IdProductSubGroup = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Qrcode = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "ProductsShared",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProductGroup = table.Column<int>(type: "int", nullable: false),
                    IdProductSubGroup = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Qrcode = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Unit = table.Column<int>(type: "int", maxLength: 20, nullable: false)
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
                });

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
                    { "37846734-172e-4149-8cec-6f43d1eb3f60", 0, "3ae26c2d-5766-4316-84dc-22d78a855b78", "jmmatheus23@gmail.com", true, "JoaoDiasUser", false, null, new byte[0], "JMMATHEUS23@GMAIL.COM", "JMMATHEUS23@GMAIL.COM", "AQAAAAIAAYagAAAAEOWpoGN/K/e6j469eZWgLMSI4dh/5127SQDTlVDXMbFLAMpOWNij/sQJ/lgmn98inQ==", null, false, new byte[0], "8b31d330-34f6-4933-9fd9-ca513cd467f2", false, "jmmatheus23@gmail.com" },
                    { "38846734-172e-4149-8cec-6f43d1eb3f60", 0, "9bf849c9-003c-4948-90fc-ec38bb42d7aa", "joaodiasworking@gmail.com", true, "JoaoDiasAdmin", false, null, new byte[0], "JOAODIASWORKING@GMAIL.COM", "JOAODIASWORKING@GMAIL.COM", "AQAAAAIAAYagAAAAEIV/fDyl9nIelduka1A6I0tvaEYSwRv415/B6zDVqw2iZRhe9MtMhFTF2M862i0Aeg==", null, false, new byte[0], "d033b9cc-4084-4881-ac58-54f8a388037c", false, "joaodiasworking@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "Image", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 5, 26, 19, 5, 40, 678, DateTimeKind.Local).AddTicks(9968), "GERAL", true, new byte[] { 0 }, "GERAL", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local) });

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
                values: new object[] { 1, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(177), "GERAL", true, 1, new byte[] { 0 }, "GERAL", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(178) });

            migrationBuilder.InsertData(
                table: "ProductsLists",
                columns: new[] { "Id", "CreatedDate", "Description", "ExternalLink", "IdUser", "Image", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(229), "Description for Shopping List 1", "", "37846734-172e-4149-8cec-6f43d1eb3f60", new byte[] { 0 }, "Shopping List 1", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(230) },
                    { 2, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(232), "Description for Shopping List 2", "", "37846734-172e-4149-8cec-6f43d1eb3f60", new byte[] { 0 }, "Shopping List 2", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(233) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(350), "TESTE 1", true, 1, 1, 1, new byte[] { 0 }, "", "Teste 1", new byte[] { 0 }, "Meter", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(351), 22.05m },
                    { 2, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(355), "TESTE 2", true, 1, 1, 1, new byte[] { 0 }, "", "Teste 2", new byte[] { 0 }, "SquareMeter", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(356), 33.33m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 3, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(357), "TESTE 3", 1, 1, 1, new byte[] { 0 }, "", "Teste 3", new byte[] { 0 }, "Unspecified", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(358), 42.33m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(361), "TESTE 4", true, 1, 1, 2, new byte[] { 0 }, "", "Teste 4", new byte[] { 0 }, "CubicMeter", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(362), 77.77m },
                    { 5, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(364), "TESTE 5", true, 1, 1, 2, new byte[] { 0 }, "", "Teste 5", new byte[] { 0 }, "Unit", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(374), 66.66m }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "IdProductsList", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 6, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(390), "TESTE 6", 1, 1, 2, new byte[] { 0 }, "", "Teste 6", new byte[] { 0 }, "Unspecified", new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(390), 35.31m });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(283), "TESTE 1", true, 1, 1, new byte[] { 0 }, "", "Teste 1", new byte[] { 0 }, 1, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(283), 22.05m },
                    { 2, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(288), "TESTE 2", true, 1, 1, new byte[] { 0 }, "", "Teste 2", new byte[] { 0 }, 2, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(289), 33.33m }
                });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 3, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(290), "TESTE 3", 1, 1, new byte[] { 0 }, "", "Teste 3", new byte[] { 0 }, 0, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(291), 42.33m });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "Enabled", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 4, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(293), "TESTE 4", true, 1, 1, new byte[] { 0 }, "", "Teste 4", new byte[] { 0 }, 3, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(293), 77.77m },
                    { 5, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(295), "TESTE 5", true, 1, 1, new byte[] { 0 }, "", "Teste 5", new byte[] { 0 }, 4, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(295), 66.66m }
                });

            migrationBuilder.InsertData(
                table: "ProductsShared",
                columns: new[] { "Id", "CreatedDate", "Description", "IdProductGroup", "IdProductSubGroup", "Image", "Link", "Name", "Qrcode", "Unit", "UpdatedDate", "Value" },
                values: new object[] { 6, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(298), "TESTE 6", 1, 1, new byte[] { 0 }, "", "Teste 6", new byte[] { 0 }, 0, new DateTime(2024, 5, 26, 19, 5, 40, 679, DateTimeKind.Local).AddTicks(298), 35.31m });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "IX_ProductsLists_IdUser",
                table: "ProductsLists",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsShared_IdProductGroup",
                table: "ProductsShared",
                column: "IdProductGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsShared_IdProductSubGroup",
                table: "ProductsShared",
                column: "IdProductSubGroup");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSubGroups_IdProductGroup",
                table: "ProductSubGroups",
                column: "IdProductGroup");
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
