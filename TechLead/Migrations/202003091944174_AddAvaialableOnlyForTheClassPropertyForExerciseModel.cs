namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvaialableOnlyForTheClassPropertyForExerciseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "AvailableOnlyForTheClass", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "AvailableOnlyForTheClass");
        }
    }
}
