namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExerciseAuthorId_Submission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "ExerciseAuthorId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "ExerciseAuthorId");
        }
    }
}
