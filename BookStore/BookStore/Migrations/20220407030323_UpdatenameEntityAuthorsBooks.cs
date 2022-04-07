using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class UpdatenameEntityAuthorsBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorBooks",
                table: "AuthorBooks");

            migrationBuilder.RenameTable(
                name: "AuthorBooks",
                newName: "AuthorsBooks");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorsBooks",
                newName: "IX_AuthorsBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorsBooks",
                table: "AuthorsBooks",
                columns: new[] { "AuthorId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsBooks_Authors_AuthorId",
                table: "AuthorsBooks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorsBooks_Books_BookId",
                table: "AuthorsBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsBooks_Authors_AuthorId",
                table: "AuthorsBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorsBooks_Books_BookId",
                table: "AuthorsBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorsBooks",
                table: "AuthorsBooks");

            migrationBuilder.RenameTable(
                name: "AuthorsBooks",
                newName: "AuthorBooks");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorsBooks_BookId",
                table: "AuthorBooks",
                newName: "IX_AuthorBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorBooks",
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId",
                table: "AuthorBooks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
