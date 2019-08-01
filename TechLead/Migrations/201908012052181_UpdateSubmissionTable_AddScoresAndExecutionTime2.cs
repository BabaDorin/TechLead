namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubmissionTable_AddScoresAndExecutionTime2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "Score1", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime1", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score2", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime2", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score3", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime3", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score4", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime4", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score5", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime5", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score6", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime6", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score7", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime7", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score8", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime8", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score9", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime9", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score10", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime10", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "ExecutionTime10");
            DropColumn("dbo.Submissions", "Score10");
            DropColumn("dbo.Submissions", "ExecutionTime9");
            DropColumn("dbo.Submissions", "Score9");
            DropColumn("dbo.Submissions", "ExecutionTime8");
            DropColumn("dbo.Submissions", "Score8");
            DropColumn("dbo.Submissions", "ExecutionTime7");
            DropColumn("dbo.Submissions", "Score7");
            DropColumn("dbo.Submissions", "ExecutionTime6");
            DropColumn("dbo.Submissions", "Score6");
            DropColumn("dbo.Submissions", "ExecutionTime5");
            DropColumn("dbo.Submissions", "Score5");
            DropColumn("dbo.Submissions", "ExecutionTime4");
            DropColumn("dbo.Submissions", "Score4");
            DropColumn("dbo.Submissions", "ExecutionTime3");
            DropColumn("dbo.Submissions", "Score3");
            DropColumn("dbo.Submissions", "ExecutionTime2");
            DropColumn("dbo.Submissions", "Score2");
            DropColumn("dbo.Submissions", "ExecutionTime1");
            DropColumn("dbo.Submissions", "Score1");
        }
    }
}
