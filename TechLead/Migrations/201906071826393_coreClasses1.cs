namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coreClasses1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Difficulty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author_Id = c.String(maxLength: 128),
                        Domain_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.Difficulty", t => t.Domain_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Domain_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "Domain_Id", "dbo.Difficulty");
            DropForeignKey("dbo.Exercises", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Exercises", new[] { "Domain_Id" });
            DropIndex("dbo.Exercises", new[] { "Author_Id" });
            DropTable("dbo.Exercises");
            DropTable("dbo.Difficulty");
        }
    }
}
