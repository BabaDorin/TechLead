namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exercises", "Name", c => c.String());
            AlterColumn("dbo.Exercises", "Condition", c => c.String());
            AlterColumn("dbo.Exercises", "Imput1", c => c.String());
            AlterColumn("dbo.Exercises", "Output1", c => c.String());
            AlterColumn("dbo.Exercises", "TestImput1", c => c.String());
            AlterColumn("dbo.Exercises", "TestImput2", c => c.String());
            AlterColumn("dbo.Exercises", "TestImput3", c => c.String());
            AlterColumn("dbo.Exercises", "TestOutput1", c => c.String());
            AlterColumn("dbo.Exercises", "TestOutput2", c => c.String());
            AlterColumn("dbo.Exercises", "TestOutput3", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exercises", "TestOutput3", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "TestOutput2", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "TestOutput1", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "TestImput3", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "TestImput2", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "TestImput1", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "Output1", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "Imput1", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "Condition", c => c.String(nullable: false));
            AlterColumn("dbo.Exercises", "Name", c => c.String(nullable: false));
        }
    }
}
