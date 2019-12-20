namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseModelSpellingCorrection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "DifficultyId", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "InputFormat", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "Input1", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "Input2", c => c.String());
            AddColumn("dbo.Exercises", "Input3", c => c.String());
            AddColumn("dbo.Exercises", "Input4", c => c.String());
            AddColumn("dbo.Exercises", "Input5", c => c.String());
            DropColumn("dbo.Exercises", "DifficultyId");
            DropColumn("dbo.Exercises", "InputFormat");
            DropColumn("dbo.Exercises", "Imput1");
            DropColumn("dbo.Exercises", "Input2");
            DropColumn("dbo.Exercises", "Input3");
            DropColumn("dbo.Exercises", "Imput4");
            DropColumn("dbo.Exercises", "Input5");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "Input5", c => c.String());
            AddColumn("dbo.Exercises", "Imput4", c => c.String());
            AddColumn("dbo.Exercises", "Input3", c => c.String());
            AddColumn("dbo.Exercises", "Input2", c => c.String());
            AddColumn("dbo.Exercises", "Imput1", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "InputFormat", c => c.String(nullable: false));
            AddColumn("dbo.Exercises", "DifficultyId", c => c.Int(nullable: false));
            DropColumn("dbo.Exercises", "Input5");
            DropColumn("dbo.Exercises", "Input4");
            DropColumn("dbo.Exercises", "Input3");
            DropColumn("dbo.Exercises", "Input2");
            DropColumn("dbo.Exercises", "Input1");
            DropColumn("dbo.Exercises", "InputFormat");
            DropColumn("dbo.Exercises", "DifficultyId");
        }
    }
}
