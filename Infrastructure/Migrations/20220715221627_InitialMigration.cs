using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApprovalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApprovalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CustomerAccount = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CountryRegion = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    EmployeeTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeType_EmployeeTypeId",
                        column: x => x.EmployeeTypeId,
                        principalTable: "EmployeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Timesheet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimesheetNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    PeriodStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PeriodEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalStatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheet_ApprovalStatus_ApprovalStatusId",
                        column: x => x.ApprovalStatusId,
                        principalTable: "ApprovalStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Timesheet_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimesheetId = table.Column<int>(type: "INTEGER", nullable: false),
                    LegalEntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActivityId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    LinePropertyId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_LegalEntity_LegalEntityId",
                        column: x => x.LegalEntityId,
                        principalTable: "LegalEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_LineProperty_LinePropertyId",
                        column: x => x.LinePropertyId,
                        principalTable: "LineProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimesheetLine_Timesheet_TimesheetId",
                        column: x => x.TimesheetId,
                        principalTable: "Timesheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimesheetLineDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimesheetLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Hours = table.Column<float>(type: "REAL", nullable: false),
                    InternalComment = table.Column<string>(type: "TEXT", nullable: true),
                    ExternalComment = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimesheetLineDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimesheetLineDetail_TimesheetLine_TimesheetLineId",
                        column: x => x.TimesheetLineId,
                        principalTable: "TimesheetLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "Number" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Maintenance & Support", "45789-US01" });

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "Number" },
                values: new object[] { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Design/Build/Test", "45795-US01" });

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "Number" },
                values: new object[] { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Plan", "45796-US01" });

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "Number" },
                values: new object[] { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Research", "45797-US01" });

            migrationBuilder.InsertData(
                table: "Activity",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name", "Number" },
                values: new object[] { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Project Management", "45799-US01" });

            migrationBuilder.InsertData(
                table: "ApprovalStatus",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Draft" });

            migrationBuilder.InsertData(
                table: "ApprovalStatus",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Posted" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Analyst" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Functional Lead" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Project Manager" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Scrum Master" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Technical Architect" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Technical Lead" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Tester" });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "AccountNumber", "CreatedBy", "CreatedOn", "CustomerAccount", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C00085-US01", false, 0, null, "Hitachi Solutions America, Ltd." });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "AccountNumber", "CreatedBy", "CreatedOn", "CustomerAccount", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "C00014-US01", false, 0, null, "Hitachi Vantara INC" });

            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Onshore" });

            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Offshore" });

            migrationBuilder.InsertData(
                table: "LegalEntity",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, "US01", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Hitachi Solutions America Ltd" });

            migrationBuilder.InsertData(
                table: "LegalEntity",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, "CA01", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Hitachi Solutions Canada Ltd" });

            migrationBuilder.InsertData(
                table: "LegalEntity",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 3, "DE01", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Hitachi Solutions Germany GmbH" });

            migrationBuilder.InsertData(
                table: "LegalEntity",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 4, "GB01", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Hitachi Solutions Europe Ltd" });

            migrationBuilder.InsertData(
                table: "LegalEntity",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 5, "SG01", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Hitachi Solutions Asia Pacific Pte..." });

            migrationBuilder.InsertData(
                table: "LineProperty",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Billable" });

            migrationBuilder.InsertData(
                table: "LineProperty",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Subcontractor fees" });

            migrationBuilder.InsertData(
                table: "LineProperty",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Extra hours" });

            migrationBuilder.InsertData(
                table: "LineProperty",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 4, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Fixed price" });

            migrationBuilder.InsertData(
                table: "LineProperty",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 5, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Internal" });

            migrationBuilder.InsertData(
                table: "LineProperty",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 6, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Non billable" });

            migrationBuilder.InsertData(
                table: "LineProperty",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 7, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Time-off" });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "CountryRegion", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, "USA", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "United States" });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "CountryRegion", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, "CAN", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Canada" });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 1, "000000.02.01", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "BSG Internal - Time Off" });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedOn", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "Name" },
                values: new object[] { 2, "323671", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, null, "Internal - Empower Phase II" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "EmployeeTypeId", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName" },
                values: new object[] { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ivan", false, 0, null, "Ball-llovera" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "EmployeeTypeId", "FirstName", "IsDeleted", "LastModifiedBy", "LastModifiedOn", "LastName" },
                values: new object[] { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Arun", false, 0, null, "Pandey" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeTypeId",
                table: "Employee",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_ApprovalStatusId",
                table: "Timesheet",
                column: "ApprovalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Timesheet_EmployeeId",
                table: "Timesheet",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_ActivityId",
                table: "TimesheetLine",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_CategoryId",
                table: "TimesheetLine",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_CustomerId",
                table: "TimesheetLine",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_LegalEntityId",
                table: "TimesheetLine",
                column: "LegalEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_LinePropertyId",
                table: "TimesheetLine",
                column: "LinePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_LocationId",
                table: "TimesheetLine",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_ProjectId",
                table: "TimesheetLine",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLine_TimesheetId",
                table: "TimesheetLine",
                column: "TimesheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimesheetLineDetail_TimesheetLineId",
                table: "TimesheetLineDetail",
                column: "TimesheetLineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimesheetLineDetail");

            migrationBuilder.DropTable(
                name: "TimesheetLine");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "LegalEntity");

            migrationBuilder.DropTable(
                name: "LineProperty");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Timesheet");

            migrationBuilder.DropTable(
                name: "ApprovalStatus");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeeType");
        }
    }
}
