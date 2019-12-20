namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingFieldsToExerciseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Points", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "SubmissionsAbove10Points", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "SubmissionsUnder10Points", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "Condition", c => c.String());
            AddColumn("dbo.Exercises", "Input1", c => c.String());
            AddColumn("dbo.Exercises", "Input2", c => c.String());
            AddColumn("dbo.Exercises", "Input3", c => c.String());
            AddColumn("dbo.Exercises", "Input4", c => c.String());
            AddColumn("dbo.Exercises", "Input5", c => c.String());
            AddColumn("dbo.Exercises", "Output1", c => c.String());
            AddColumn("dbo.Exercises", "Output2", c => c.String());
            AddColumn("dbo.Exercises", "Output3", c => c.String());
            AddColumn("dbo.Exercises", "Output4", c => c.String());
            AddColumn("dbo.Exercises", "Output5", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "Output5");
            DropColumn("dbo.Exercises", "Output4");
            DropColumn("dbo.Exercises", "Output3");
            DropColumn("dbo.Exercises", "Output2");
            DropColumn("dbo.Exercises", "Output1");
            DropColumn("dbo.Exercises", "Input5");
            DropColumn("dbo.Exercises", "Input4");
            DropColumn("dbo.Exercises", "Input3");
            DropColumn("dbo.Exercises", "Input2");
            DropColumn("dbo.Exercises", "Input1");
            DropColumn("dbo.Exercises", "Condition");
            DropColumn("dbo.Exercises", "SubmissionsUnder10Points");
            DropColumn("dbo.Exercises", "SubmissionsAbove10Points");
            DropColumn("dbo.Exercises", "Points");
        }
    }
}
