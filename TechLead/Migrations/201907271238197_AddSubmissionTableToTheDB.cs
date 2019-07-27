namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubmissionTableToTheDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Submissions",
                c => new
                    {
                        SubmissionID = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Exercise_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SubmissionID)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Exercise_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Submissions", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Submissions", "Exercise_Id", "dbo.Exercises");
            DropIndex("dbo.Submissions", new[] { "User_Id" });
            DropIndex("dbo.Submissions", new[] { "Exercise_Id" });
            DropTable("dbo.Submissions");
        }
    }
}
