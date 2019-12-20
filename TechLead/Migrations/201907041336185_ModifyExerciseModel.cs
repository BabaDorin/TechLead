namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyExerciseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "DifficultyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "DifficultyId");
        }
    }
}
