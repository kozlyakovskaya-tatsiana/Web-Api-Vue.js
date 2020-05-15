namespace web_server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersBooks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropIndex("dbo.BookAuthors", new[] { "Book_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            CreateTable(
                "dbo.ApplicationUserBooks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Book_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Book_Id);
            
            AddColumn("dbo.Books", "Author_Id", c => c.Int());
            CreateIndex("dbo.Books", "Author_Id");
            AddForeignKey("dbo.Books", "Author_Id", "dbo.Authors", "Id");
            DropTable("dbo.BookAuthors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_Id = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Id, t.Author_Id });
            
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.ApplicationUserBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.ApplicationUserBooks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserBooks", new[] { "Book_Id" });
            DropIndex("dbo.ApplicationUserBooks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropColumn("dbo.Books", "Author_Id");
            DropTable("dbo.ApplicationUserBooks");
            CreateIndex("dbo.BookAuthors", "Author_Id");
            CreateIndex("dbo.BookAuthors", "Book_Id");
            AddForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BookAuthors", "Book_Id", "dbo.Books", "Id", cascadeDelete: true);
        }
    }
}
