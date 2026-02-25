using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.CheckConstraint("CK_Categoriy_Dates", "[ModifiedAt] >= [CreatedAt]");
                });

            migrationBuilder.CreateTable(
                name: "ClassLocationAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLocationAddresses", x => x.Id);
                    table.CheckConstraint("CK_Class_Location_Address_Dates", "[ModifiedAt] >= [CreatedAt]");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.CheckConstraint("CK_Course_Dates", "[ModifiedAt] >= [CreatedAt]");
                });

            migrationBuilder.CreateTable(
                name: "EventLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocations", x => x.Id);
                    table.CheckConstraint("CK_Event_Location_Dates", "[ModifiedAt] >= [CreatedAt]");
                    table.CheckConstraint("CK_Seat_Min1", "[Seats] >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.CheckConstraint("CK_Program_Dates", "[ModifiedAt] >= [CreatedAt]");
                    table.ForeignKey(
                        name: "FK_Programs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    ClassLocationAddressId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassLocations", x => x.Id);
                    table.CheckConstraint("CK_Class_Locations_Dates", "[ModifiedAt] >= [CreatedAt]");
                    table.ForeignKey(
                        name: "FK_ClassLocations_ClassLocationAddresses_ClassLocationAddressId",
                        column: x => x.ClassLocationAddressId,
                        principalTable: "ClassLocationAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryCourses",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCourses", x => new { x.CourseId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_CategoryCourses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseProfiles",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProfiles", x => new { x.CourseId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_CourseProfiles_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    EventLocationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEvents", x => x.Id);
                    table.CheckConstraint("CK_Course_Event_Dates", "[ModifiedAt] >= [CreatedAt]");
                    table.ForeignKey(
                        name: "FK_CourseEvents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseEvents_EventLocations_EventLocationId",
                        column: x => x.EventLocationId,
                        principalTable: "EventLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursePrograms",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrograms", x => new { x.ProgramId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_CoursePrograms_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePrograms_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    ClassLocationId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2(0)", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.CheckConstraint("CK_Class_Dates", "[ModifiedAt] >= [CreatedAt]");
                    table.CheckConstraint("CK_Seat_Min", "[Seats] >= 0");
                    table.ForeignKey(
                        name: "FK_Classes_ClassLocations_ClassLocationId",
                        column: x => x.ClassLocationId,
                        principalTable: "ClassLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClassCourseEvents",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    CourseEventId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCourseEvents", x => new { x.ClassId, x.CourseEventId });
                    table.ForeignKey(
                        name: "FK_ClassCourseEvents_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassCourseEvents_CourseEvents_CourseEventId",
                        column: x => x.CourseEventId,
                        principalTable: "CourseEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassProfiles",
                columns: table => new
                {
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 68, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassProfiles", x => new { x.ClassId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ClassProfiles_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCourses_CategoryId",
                table: "CategoryCourses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassCourseEvents_CourseEventId",
                table: "ClassCourseEvents",
                column: "CourseEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassLocationId",
                table: "Classes",
                column: "ClassLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_Name",
                table: "Classes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ProgramId",
                table: "Classes",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassLocations_ClassLocationAddressId",
                table: "ClassLocations",
                column: "ClassLocationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassProfiles_ProfileId",
                table: "ClassProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEvents_CourseId",
                table: "CourseEvents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEvents_EventLocationId",
                table: "CourseEvents",
                column: "EventLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseProfiles_ProfileId",
                table: "CourseProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrograms_CourseId",
                table: "CoursePrograms",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Name",
                table: "Courses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventLocations_Name",
                table: "EventLocations",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_CategoryId",
                table: "Programs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_Name",
                table: "Programs",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCourses");

            migrationBuilder.DropTable(
                name: "ClassCourseEvents");

            migrationBuilder.DropTable(
                name: "ClassProfiles");

            migrationBuilder.DropTable(
                name: "CourseProfiles");

            migrationBuilder.DropTable(
                name: "CoursePrograms");

            migrationBuilder.DropTable(
                name: "CourseEvents");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.DropTable(
                name: "ClassLocations");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "ClassLocationAddresses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");
        }
    }
}
