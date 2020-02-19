namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExecutionTimeAndMemoryLimitForExerciseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "ExecutionTime", c => c.Int(nullable: false));
            AddColumn("dbo.Exercises", "MemoryLimit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "MemoryLimit");
            DropColumn("dbo.Exercises", "ExecutionTime");
        }
    }
}
