using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Smis.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserToIdentity : Migration
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
                name: "Orari",
                columns: table => new
                {
                    OrariID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateOnly>(type: "date", nullable: true),
                    Koha = table.Column<TimeOnly>(type: "time", nullable: true),
                    Lloji = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Ndërrimi = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orari__9849076D327A3909", x => x.OrariID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmriRolit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Universiteti",
                columns: table => new
                {
                    UniID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Adress = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Universi__9E7FA84A35466E61", x => x.UniID);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Emri = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Mbiemri = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Roli = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Datelindja = table.Column<DateOnly>(type: "date", nullable: true),
                    NrLeternjoftimit = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    VendLindja = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Gjinia = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Shteti = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Vendbanimi = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Adresa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ZIPKodi = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Telefoni = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Nenshtetesia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Foto = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departamenti",
                columns: table => new
                {
                    DepartamentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UniID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Departam__67077AE2D726109C", x => x.DepartamentiID);
                    table.ForeignKey(
                        name: "FK__Departame__UniID__3A81B327",
                        column: x => x.UniID,
                        principalTable: "Universiteti",
                        principalColumn: "UniID");
                });

            migrationBuilder.CreateTable(
                name: "Lokacionet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Qyteti = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversitetiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lokacionet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lokacionet_Universiteti_UniversitetiId",
                        column: x => x.UniversitetiId,
                        principalTable: "Universiteti",
                        principalColumn: "UniID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Semestri",
                columns: table => new
                {
                    SemestriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Semestri = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UniID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Semestri__E6EAA2D31A5FF8AA", x => x.SemestriID);
                    table.ForeignKey(
                        name: "FK__Semestri__UniID__3D5E1FD2",
                        column: x => x.UniID,
                        principalTable: "Universiteti",
                        principalColumn: "UniID");
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartamentiID = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UniID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Admin__719FE4E8E38461D9", x => x.AdminID);
                    table.ForeignKey(
                        name: "FK__Admin__Departame__4CA06362",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamenti",
                        principalColumn: "DepartamentiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Admin__UniID__4E88ABD4",
                        column: x => x.UniID,
                        principalTable: "Universiteti",
                        principalColumn: "UniID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Admin__UserID__4D94879B",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StafiAkademik",
                columns: table => new
                {
                    StafiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VitiRegjistrimit = table.Column<int>(type: "int", nullable: true),
                    Roli = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Titulli = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    UniID = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__StafiAka__979A23F0A2DE9EE0", x => x.StafiID);
                    table.ForeignKey(
                        name: "FK_StafiAkademik_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__StafiAkad__Depar__46E78A0C",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamenti",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK__StafiAkad__UniID__47DBAE45",
                        column: x => x.UniID,
                        principalTable: "Universiteti",
                        principalColumn: "UniID");
                });

            migrationBuilder.CreateTable(
                name: "Grupi",
                columns: table => new
                {
                    GrupiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SemestriID = table.Column<int>(type: "int", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    OrariID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Grupi__580495DA09264F41", x => x.GrupiID);
                    table.ForeignKey(
                        name: "FK__Grupi__Departame__4316F928",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamenti",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK__Grupi__OrariID__440B1D61",
                        column: x => x.OrariID,
                        principalTable: "Orari",
                        principalColumn: "OrariID");
                    table.ForeignKey(
                        name: "FK__Grupi__SemestriI__4222D4EF",
                        column: x => x.SemestriID,
                        principalTable: "Semestri",
                        principalColumn: "SemestriID");
                });

            migrationBuilder.CreateTable(
                name: "Lenda",
                columns: table => new
                {
                    LendaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Kredite = table.Column<int>(type: "int", nullable: true),
                    Kategoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    SemestriID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lenda__F222ADBEE91CCC4B", x => x.LendaID);
                    table.ForeignKey(
                        name: "FK__Lenda__Departame__5629CD9C",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamenti",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK__Lenda__SemestriI__571DF1D5",
                        column: x => x.SemestriID,
                        principalTable: "Semestri",
                        principalColumn: "SemestriID");
                });

            migrationBuilder.CreateTable(
                name: "Salla",
                columns: table => new
                {
                    SallaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrSalles = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Kapaciteti = table.Column<int>(type: "int", nullable: true),
                    Objekti = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    StafiAkademikID = table.Column<int>(type: "int", nullable: true),
                    UniID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Salla__B8218E197C43C293", x => x.SallaID);
                    table.ForeignKey(
                        name: "FK__Salla__StafiAkad__74AE54BC",
                        column: x => x.StafiAkademikID,
                        principalTable: "StafiAkademik",
                        principalColumn: "StafiID");
                    table.ForeignKey(
                        name: "FK__Salla__UniID__75A278F5",
                        column: x => x.UniID,
                        principalTable: "Universiteti",
                        principalColumn: "UniID");
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    StudentiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VitiRegjistrimit = table.Column<int>(type: "int", nullable: true),
                    Statusi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UniID = table.Column<int>(type: "int", nullable: true),
                    DepartamentiID = table.Column<int>(type: "int", nullable: true),
                    GrupiID = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Studenti__EBE2AD8DFC0E89E1", x => x.StudentiID);
                    table.ForeignKey(
                        name: "FK_Studenti_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Studenti__Depart__52593CB8",
                        column: x => x.DepartamentiID,
                        principalTable: "Departamenti",
                        principalColumn: "DepartamentiID");
                    table.ForeignKey(
                        name: "FK__Studenti__GrupiI__534D60F1",
                        column: x => x.GrupiID,
                        principalTable: "Grupi",
                        principalColumn: "GrupiID");
                    table.ForeignKey(
                        name: "FK__Studenti__UniID__5165187F",
                        column: x => x.UniID,
                        principalTable: "Universiteti",
                        principalColumn: "UniID");
                });

            migrationBuilder.CreateTable(
                name: "Ligjerata",
                columns: table => new
                {
                    StafiID = table.Column<int>(type: "int", nullable: false),
                    LendaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ligjerat__68B8092B794D08D0", x => new { x.StafiID, x.LendaID });
                    table.ForeignKey(
                        name: "FK__Ligjerata__Lenda__5FB337D6",
                        column: x => x.LendaID,
                        principalTable: "Lenda",
                        principalColumn: "LendaID");
                    table.ForeignKey(
                        name: "FK__Ligjerata__Stafi__5EBF139D",
                        column: x => x.StafiID,
                        principalTable: "StafiAkademik",
                        principalColumn: "StafiID");
                });

            migrationBuilder.CreateTable(
                name: "Provimi",
                columns: table => new
                {
                    ProvimiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Statusi = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    StafiAkademikID = table.Column<int>(type: "int", nullable: true),
                    LendaID = table.Column<int>(type: "int", nullable: true),
                    OrariID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Provimi__30F18CFB0E2D146E", x => x.ProvimiID);
                    table.ForeignKey(
                        name: "FK__Provimi__LendaID__5AEE82B9",
                        column: x => x.LendaID,
                        principalTable: "Lenda",
                        principalColumn: "LendaID");
                    table.ForeignKey(
                        name: "FK__Provimi__OrariID__5BE2A6F2",
                        column: x => x.OrariID,
                        principalTable: "Orari",
                        principalColumn: "OrariID");
                    table.ForeignKey(
                        name: "FK__Provimi__StafiAk__59FA5E80",
                        column: x => x.StafiAkademikID,
                        principalTable: "StafiAkademik",
                        principalColumn: "StafiID");
                });

            migrationBuilder.CreateTable(
                name: "Orari_Salla",
                columns: table => new
                {
                    OrariID = table.Column<int>(type: "int", nullable: false),
                    SallaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orari_Sa__13CB1F8C031D9C39", x => new { x.OrariID, x.SallaID });
                    table.ForeignKey(
                        name: "FK__Orari_Sal__Orari__787EE5A0",
                        column: x => x.OrariID,
                        principalTable: "Orari",
                        principalColumn: "OrariID");
                    table.ForeignKey(
                        name: "FK__Orari_Sal__Salla__797309D9",
                        column: x => x.SallaID,
                        principalTable: "Salla",
                        principalColumn: "SallaID");
                });

            migrationBuilder.CreateTable(
                name: "Pagesa",
                columns: table => new
                {
                    PagesaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Shuma = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    DataPageses = table.Column<DateOnly>(type: "date", nullable: true),
                    StudentiID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pagesa__D4F1A29DF907B783", x => x.PagesaID);
                    table.ForeignKey(
                        name: "FK__Pagesa__Studenti__71D1E811",
                        column: x => x.StudentiID,
                        principalTable: "Studenti",
                        principalColumn: "StudentiID");
                });

            migrationBuilder.CreateTable(
                name: "ParaqitjaEProvimit",
                columns: table => new
                {
                    ParaqitjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataProvimit = table.Column<DateOnly>(type: "date", nullable: true),
                    StudentiID = table.Column<int>(type: "int", nullable: true),
                    LendaID = table.Column<int>(type: "int", nullable: true),
                    StafiAkademikId = table.Column<int>(type: "int", nullable: false),
                    EshtePerfunduar = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paraqitj__F86D81136DE3097B", x => x.ParaqitjaID);
                    table.ForeignKey(
                        name: "FK_ParaqitjaEProvimit_StafiAkademik_StafiAkademikId",
                        column: x => x.StafiAkademikId,
                        principalTable: "StafiAkademik",
                        principalColumn: "StafiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Paraqitja__Lenda__6EF57B66",
                        column: x => x.LendaID,
                        principalTable: "Lenda",
                        principalColumn: "LendaID");
                    table.ForeignKey(
                        name: "FK__Paraqitja__Stude__6E01572D",
                        column: x => x.StudentiID,
                        principalTable: "Studenti",
                        principalColumn: "StudentiID");
                });

            migrationBuilder.CreateTable(
                name: "RegjistrimiSemestrit",
                columns: table => new
                {
                    RegjistrimiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SemestriID = table.Column<int>(type: "int", nullable: false),
                    DataRegjistrimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lokacioni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nderrimi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegjistrimiSemestrit", x => x.RegjistrimiId);
                    table.ForeignKey(
                        name: "FK_RegjistrimiSemestrit_Semestri_SemestriID",
                        column: x => x.SemestriID,
                        principalTable: "Semestri",
                        principalColumn: "SemestriID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegjistrimiSemestrit_Studenti_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Studenti",
                        principalColumn: "StudentiID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ligjerata_Orari",
                columns: table => new
                {
                    OrariID = table.Column<int>(type: "int", nullable: false),
                    StafiID = table.Column<int>(type: "int", nullable: false),
                    LendaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ligjerat__3EC287FF252515D0", x => new { x.OrariID, x.StafiID, x.LendaID });
                    table.ForeignKey(
                        name: "FK__Ligjerata_Orari__6383C8BA",
                        columns: x => new { x.StafiID, x.LendaID },
                        principalTable: "Ligjerata",
                        principalColumns: new[] { "StafiID", "LendaID" });
                    table.ForeignKey(
                        name: "FK__Ligjerata__Orari__628FA481",
                        column: x => x.OrariID,
                        principalTable: "Orari",
                        principalColumn: "OrariID");
                });

            migrationBuilder.CreateTable(
                name: "Nota",
                columns: table => new
                {
                    NotaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotaNr = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    NotaShkronje = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    DataVendosjes = table.Column<DateOnly>(type: "date", nullable: true),
                    StudentiID = table.Column<int>(type: "int", nullable: true),
                    ParaqitjaID = table.Column<int>(type: "int", nullable: true),
                    EshteRefuzuar = table.Column<bool>(type: "bit", nullable: false),
                    ProvimiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Nota__EF36CC7AADB7961F", x => x.NotaID);
                    table.ForeignKey(
                        name: "FK_Nota_ParaqitjaEprovimit",
                        column: x => x.ParaqitjaID,
                        principalTable: "ParaqitjaEProvimit",
                        principalColumn: "ParaqitjaID");
                    table.ForeignKey(
                        name: "FK_Nota_Provimi_ProvimiId",
                        column: x => x.ProvimiId,
                        principalTable: "Provimi",
                        principalColumn: "ProvimiID");
                    table.ForeignKey(
                        name: "FK__Nota__StudentiID__6A30C649",
                        column: x => x.StudentiID,
                        principalTable: "Studenti",
                        principalColumn: "StudentiID");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "SuperAdmin", "SUPERADMIN" },
                    { "2", null, "Admin", "ADMIN" },
                    { "3", null, "StafAkademik", "STAFAKADEMIK" },
                    { "4", null, "Student", "STUDENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_DepartamentiID",
                table: "Admin",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Id",
                table: "Admin",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_UniID",
                table: "Admin",
                column: "UniID");

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
                name: "IX_AspNetUsers_RoleId",
                table: "AspNetUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Departamenti_UniID",
                table: "Departamenti",
                column: "UniID");

            migrationBuilder.CreateIndex(
                name: "IX_Grupi_DepartamentiID",
                table: "Grupi",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_Grupi_OrariID",
                table: "Grupi",
                column: "OrariID");

            migrationBuilder.CreateIndex(
                name: "IX_Grupi_SemestriID",
                table: "Grupi",
                column: "SemestriID");

            migrationBuilder.CreateIndex(
                name: "IX_Lenda_DepartamentiID",
                table: "Lenda",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_Lenda_SemestriID",
                table: "Lenda",
                column: "SemestriID");

            migrationBuilder.CreateIndex(
                name: "IX_Ligjerata_LendaID",
                table: "Ligjerata",
                column: "LendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ligjerata_Orari_StafiID_LendaID",
                table: "Ligjerata_Orari",
                columns: new[] { "StafiID", "LendaID" });

            migrationBuilder.CreateIndex(
                name: "IX_Lokacionet_UniversitetiId",
                table: "Lokacionet",
                column: "UniversitetiId");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_ParaqitjaID",
                table: "Nota",
                column: "ParaqitjaID",
                unique: true,
                filter: "[ParaqitjaID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_ProvimiId",
                table: "Nota",
                column: "ProvimiId");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_StudentiID",
                table: "Nota",
                column: "StudentiID");

            migrationBuilder.CreateIndex(
                name: "IX_Orari_Salla_SallaID",
                table: "Orari_Salla",
                column: "SallaID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagesa_StudentiID",
                table: "Pagesa",
                column: "StudentiID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaEProvimit_LendaID",
                table: "ParaqitjaEProvimit",
                column: "LendaID");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaEProvimit_StafiAkademikId",
                table: "ParaqitjaEProvimit",
                column: "StafiAkademikId");

            migrationBuilder.CreateIndex(
                name: "IX_ParaqitjaEProvimit_StudentiID",
                table: "ParaqitjaEProvimit",
                column: "StudentiID");

            migrationBuilder.CreateIndex(
                name: "IX_Provimi_LendaID",
                table: "Provimi",
                column: "LendaID");

            migrationBuilder.CreateIndex(
                name: "IX_Provimi_OrariID",
                table: "Provimi",
                column: "OrariID");

            migrationBuilder.CreateIndex(
                name: "IX_Provimi_StafiAkademikID",
                table: "Provimi",
                column: "StafiAkademikID");

            migrationBuilder.CreateIndex(
                name: "IX_RegjistrimiSemestrit_SemestriID",
                table: "RegjistrimiSemestrit",
                column: "SemestriID");

            migrationBuilder.CreateIndex(
                name: "IX_RegjistrimiSemestrit_StudentID",
                table: "RegjistrimiSemestrit",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Salla_StafiAkademikID",
                table: "Salla",
                column: "StafiAkademikID");

            migrationBuilder.CreateIndex(
                name: "IX_Salla_UniID",
                table: "Salla",
                column: "UniID");

            migrationBuilder.CreateIndex(
                name: "IX_Semestri_UniID",
                table: "Semestri",
                column: "UniID");

            migrationBuilder.CreateIndex(
                name: "IX_StafiAkademik_DepartamentiID",
                table: "StafiAkademik",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_StafiAkademik_Id",
                table: "StafiAkademik",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StafiAkademik_UniID",
                table: "StafiAkademik",
                column: "UniID");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_DepartamentiID",
                table: "Studenti",
                column: "DepartamentiID");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_GrupiID",
                table: "Studenti",
                column: "GrupiID");

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_Id",
                table: "Studenti",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_UniID",
                table: "Studenti",
                column: "UniID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

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
                name: "Ligjerata_Orari");

            migrationBuilder.DropTable(
                name: "Lokacionet");

            migrationBuilder.DropTable(
                name: "Nota");

            migrationBuilder.DropTable(
                name: "Orari_Salla");

            migrationBuilder.DropTable(
                name: "Pagesa");

            migrationBuilder.DropTable(
                name: "RegjistrimiSemestrit");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Ligjerata");

            migrationBuilder.DropTable(
                name: "ParaqitjaEProvimit");

            migrationBuilder.DropTable(
                name: "Provimi");

            migrationBuilder.DropTable(
                name: "Salla");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Lenda");

            migrationBuilder.DropTable(
                name: "StafiAkademik");

            migrationBuilder.DropTable(
                name: "Grupi");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departamenti");

            migrationBuilder.DropTable(
                name: "Orari");

            migrationBuilder.DropTable(
                name: "Semestri");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Universiteti");
        }
    }
}
