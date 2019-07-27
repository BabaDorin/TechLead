namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Submissions", "Exercise_Id", "dbo.Exercises");
            DropIndex("dbo.Submissions", new[] { "Exercise_Id" });
            AddColumn("dbo.Submissions", "Exercise", c => c.String());
            AddColumn("dbo.Submissions", "ExerciseId", c => c.Int(nullable: false));
            DropColumn("dbo.Submissions", "Exercise_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Submissions", "Exercise_Id", c => c.Int());
            DropColumn("dbo.Submissions", "ExerciseId");
            DropColumn("dbo.Submissions", "Exercise");
            CreateIndex("dbo.Submissions", "Exercise_Id");
            AddForeignKey("dbo.Submissions", "Exercise_Id", "dbo.Exercises", "Id");
        }
    }
}
