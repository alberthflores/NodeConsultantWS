using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersistenceLayer.Migrations
{
    public partial class DataBaseInitialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Node",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Node_Node_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NodeValue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NodeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeValue_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "CreatedAt", "Description", "ParentId" },
                values: new object[] { 1, new DateTime(2021, 6, 23, 17, 14, 24, 749, DateTimeKind.Local).AddTicks(4610), "Nodo 1", null });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "CreatedAt", "Description", "ParentId" },
                values: new object[] { 2, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2641), "Nodo 2", 1 });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "CreatedAt", "Description", "ParentId" },
                values: new object[] { 3, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2655), "Nodo 3", 1 });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "CreatedAt", "Description", "ParentId" },
                values: new object[] { 4, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2658), "Nodo 4", 2 });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "CreatedAt", "Description", "ParentId" },
                values: new object[] { 5, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2659), "Nodo 5", 2 });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "CreatedAt", "Description", "ParentId" },
                values: new object[] { 6, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2660), "Nodo 6", 3 });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "CreatedAt", "Description", "ParentId" },
                values: new object[,]
                {
                    { 7, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2662), "Nodo 7", 4 },
                    { 8, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2663), "Nodo 8", 4 },
                    { 9, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2664), "Nodo 9", 5 },
                    { 10, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2665), "Nodo 10", 5 },
                    { 11, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2666), "Nodo 11", 6 },
                    { 12, new DateTime(2021, 6, 23, 17, 14, 24, 750, DateTimeKind.Local).AddTicks(2667), "Nodo 12", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Node_ParentId",
                table: "Node",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeValue_NodeId",
                table: "NodeValue",
                column: "NodeId");

            migrationBuilder.Sql(
                @"CREATE OR ALTER PROCEDURE spIncreaseNodeVues
                AS
                BEGIN
                BEGIN TRANSACTION

                    BEGIN TRY

                        DECLARE @Id INT;
                            DECLARE @RandomNumber INT;
                            DECLARE @Now DATETIME = GETDATE();
                            DECLARE db_cursor_nodes CURSOR FOR

                            SELECT Id

                            FROM[Node]

                        OPEN db_cursor_nodes FETCH NEXT FROM db_cursor_nodes INTO @Id

                            WHILE @@FETCH_STATUS = 0

                            BEGIN
                                /*Set random number beetwen 30-10*/
                                SET @RandomNumber = (SELECT FLOOR(RAND() * (30 - 10 + 1)) + 10);
                            /*Insert random number in to node value*/
                            INSERT INTO NodeValue(NodeId,[Value], CreatedAt) values(@Id, @RandomNumber, @Now);

                            FETCH NEXT FROM db_cursor_nodes INTO @Id

                            END
                        CLOSE db_cursor_nodes
                        DEALLOCATE db_cursor_nodes
                    END TRY
                    BEGIN CATCH

                        RAISERROR('ERROR EN LA BASE DE DATOS', 16, 1);
                            ROLLBACK TRAN;
                            END CATCH

                    IF(@@TRANCOUNT <> 0)

                    BEGIN
                        COMMIT TRAN;
                            END
                        END
                GO
                ");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeValue");

            migrationBuilder.DropTable(
                name: "Node");
        }
    }
}
