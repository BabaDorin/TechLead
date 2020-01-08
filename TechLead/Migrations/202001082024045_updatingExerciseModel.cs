namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatingExerciseModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exercises", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Exercises", new[] { "Author_Id" });
            AddColumn("dbo.Exercises", "Author", c => c.String());
            DropColumn("dbo.Exercises", "Author_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "Author_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Exercises", "Author");
            CreateIndex("dbo.Exercises", "Author_Id");
            AddForeignKey("dbo.Exercises", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
