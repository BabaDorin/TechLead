namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubmissionModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Submissions", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Submissions", new[] { "User_Id" });
            AddColumn("dbo.Submissions", "SubmissionAuthorUserName", c => c.String());
            AddColumn("dbo.Submissions", "ScoredPoints", c => c.Double(nullable: false));
            AddColumn("dbo.Submissions", "NumberOfTestCases", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "DistributedPointsPerTestCase", c => c.Double(nullable: false));
            AddColumn("dbo.Submissions", "InputCollection", c => c.String());
            AddColumn("dbo.Submissions", "OutputCollection", c => c.String());
            AddColumn("dbo.Submissions", "ExpectedOutput", c => c.String());
            AddColumn("dbo.Submissions", "PointsPerTestCase", c => c.String());
            AddColumn("dbo.Submissions", "ExecutionTimePerTestCase", c => c.String());
            AddColumn("dbo.Submissions", "StatusPerTestCase", c => c.String());
            AddColumn("dbo.Submissions", "ErrorMessage", c => c.String());
            DropColumn("dbo.Submissions", "Score1");
            DropColumn("dbo.Submissions", "ExecutionTime1");
            DropColumn("dbo.Submissions", "Score2");
            DropColumn("dbo.Submissions", "ExecutionTime2");
            DropColumn("dbo.Submissions", "Score3");
            DropColumn("dbo.Submissions", "ExecutionTime3");
            DropColumn("dbo.Submissions", "Score4");
            DropColumn("dbo.Submissions", "ExecutionTime4");
            DropColumn("dbo.Submissions", "Score5");
            DropColumn("dbo.Submissions", "ExecutionTime5");
            DropColumn("dbo.Submissions", "Score6");
            DropColumn("dbo.Submissions", "ExecutionTime6");
            DropColumn("dbo.Submissions", "Score7");
            DropColumn("dbo.Submissions", "ExecutionTime7");
            DropColumn("dbo.Submissions", "Score8");
            DropColumn("dbo.Submissions", "ExecutionTime8");
            DropColumn("dbo.Submissions", "Score9");
            DropColumn("dbo.Submissions", "ExecutionTime9");
            DropColumn("dbo.Submissions", "Score10");
            DropColumn("dbo.Submissions", "ExecutionTime10");
            DropColumn("dbo.Submissions", "Points");
            DropColumn("dbo.Submissions", "Time");
            DropColumn("dbo.Submissions", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Submissions", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Submissions", "Time", c => c.String());
            AddColumn("dbo.Submissions", "Points", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime10", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score10", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime9", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score9", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime8", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score8", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime7", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score7", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime6", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score6", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime5", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score5", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime4", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score4", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime3", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score3", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime2", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score2", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "ExecutionTime1", c => c.Int(nullable: false));
            AddColumn("dbo.Submissions", "Score1", c => c.Int(nullable: false));
            DropColumn("dbo.Submissions", "ErrorMessage");
            DropColumn("dbo.Submissions", "StatusPerTestCase");
            DropColumn("dbo.Submissions", "ExecutionTimePerTestCase");
            DropColumn("dbo.Submissions", "PointsPerTestCase");
            DropColumn("dbo.Submissions", "ExpectedOutput");
            DropColumn("dbo.Submissions", "OutputCollection");
            DropColumn("dbo.Submissions", "InputCollection");
            DropColumn("dbo.Submissions", "DistributedPointsPerTestCase");
            DropColumn("dbo.Submissions", "NumberOfTestCases");
            DropColumn("dbo.Submissions", "ScoredPoints");
            DropColumn("dbo.Submissions", "SubmissionAuthorUserName");
            CreateIndex("dbo.Submissions", "User_Id");
            AddForeignKey("dbo.Submissions", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
