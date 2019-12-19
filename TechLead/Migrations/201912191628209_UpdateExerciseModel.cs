namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExerciseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "NumberOfTests", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "NumberOfTests");
        }
    }
}
