namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoreClasses2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Difficulty", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Difficulty", "Name");
        }
    }
}
