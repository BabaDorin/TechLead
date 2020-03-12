namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBack_MEMES : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Memes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ImageBase64 = c.String(),
                    SuccesIndexId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SuccesIndexes", t => t.SuccesIndexId, cascadeDelete: true)
                .Index(t => t.SuccesIndexId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Memes", "SuccesIndexId", "dbo.SuccesIndexes");
            DropForeignKey("dbo.Submissions", "Meme_Id", "dbo.Memes");
            DropIndex("dbo.Submissions", new[] { "Meme_Id" });
            DropIndex("dbo.Memes", new[] { "SuccesIndexId" });
            DropColumn("dbo.Submissions", "Meme_Id");
            DropTable("dbo.Memes");
        }
    }
}
