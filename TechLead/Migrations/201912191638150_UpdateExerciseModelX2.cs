namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExerciseModelX2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exercises", "TestImput1");
            DropColumn("dbo.Exercises", "TestImput2");
            DropColumn("dbo.Exercises", "TestImput3");
            DropColumn("dbo.Exercises", "TestImput4");
            DropColumn("dbo.Exercises", "TestImput5");
            DropColumn("dbo.Exercises", "TestImput6");
            DropColumn("dbo.Exercises", "TestImput7");
            DropColumn("dbo.Exercises", "TestImput8");
            DropColumn("dbo.Exercises", "TestImput9");
            DropColumn("dbo.Exercises", "TestImput10");
            DropColumn("dbo.Exercises", "TestOutput1");
            DropColumn("dbo.Exercises", "TestOutput2");
            DropColumn("dbo.Exercises", "TestOutput3");
            DropColumn("dbo.Exercises", "TestOutput4");
            DropColumn("dbo.Exercises", "TestOutput5");
            DropColumn("dbo.Exercises", "TestOutput6");
            DropColumn("dbo.Exercises", "TestOutput7");
            DropColumn("dbo.Exercises", "TestOutput8");
            DropColumn("dbo.Exercises", "TestOutput9");
            DropColumn("dbo.Exercises", "TestOutput10");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "TestOutput10", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput9", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput8", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput7", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput6", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput5", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput4", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput3", c => c.String());
            AddColumn("dbo.Exercises", "TestOutput2", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "TestOutput1", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "TestImput10", c => c.String());
            AddColumn("dbo.Exercises", "TestImput9", c => c.String());
            AddColumn("dbo.Exercises", "TestImput8", c => c.String());
            AddColumn("dbo.Exercises", "TestImput7", c => c.String());
            AddColumn("dbo.Exercises", "TestImput6", c => c.String());
            AddColumn("dbo.Exercises", "TestImput5", c => c.String());
            AddColumn("dbo.Exercises", "TestImput4", c => c.String());
            AddColumn("dbo.Exercises", "TestImput3", c => c.String());
            AddColumn("dbo.Exercises", "TestImput2", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "TestImput1", c => c.String(nullable: false));
        }
    }
}
