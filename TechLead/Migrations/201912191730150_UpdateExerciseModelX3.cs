namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExerciseModelX3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "InputColection", c => c.String());
            AddColumn("dbo.Exercises", "OutputColection", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "OutputColection");
            DropColumn("dbo.Exercises", "InputColection");
        }
    }
}
