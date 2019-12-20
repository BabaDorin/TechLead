namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyExerciseTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "InputFormat", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "OutputFormat", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "Constraints", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "Explanation1", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "Explanation2", c => c.String());
            AddColumn("dbo.Exercises", "Explanation3", c => c.String());
            AddColumn("dbo.Exercises", "Explanation4", c => c.String());
            AddColumn("dbo.Exercises", "Explanation5", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "Explanation5");
            DropColumn("dbo.Exercises", "Explanation4");
            DropColumn("dbo.Exercises", "Explanation3");
            DropColumn("dbo.Exercises", "Explanation2");
            DropColumn("dbo.Exercises", "Explanation1");
            DropColumn("dbo.Exercises", "Constraints");
            DropColumn("dbo.Exercises", "OutputFormat");
            DropColumn("dbo.Exercises", "InputFormat");
        }
    }
}
