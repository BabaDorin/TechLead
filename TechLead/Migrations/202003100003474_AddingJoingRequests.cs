namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingJoingRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JoinRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.String(maxLength: 128),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.ClassId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.JoinRequests", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.JoinRequests", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.JoinRequests", new[] { "ClassId" });
            DropIndex("dbo.JoinRequests", new[] { "AuthorId" });
            DropTable("dbo.JoinRequests");
        }
    }
}
