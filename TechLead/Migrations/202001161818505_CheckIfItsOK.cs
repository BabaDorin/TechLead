namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckIfItsOK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "isArchieved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "isArchieved");
        }
    }
}
