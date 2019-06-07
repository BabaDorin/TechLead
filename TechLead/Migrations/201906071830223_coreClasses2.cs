namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coreClasses2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Domains", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Domains", "Name");
        }
    }
}
