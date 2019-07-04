namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Exercises", "Datetime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "Datetime", c => c.DateTime(nullable: false));
        }
    }
}
