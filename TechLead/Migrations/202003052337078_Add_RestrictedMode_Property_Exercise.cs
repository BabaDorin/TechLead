namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_RestrictedMode_Property_Exercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "RestrictedMode", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "RestrictedMode");
        }
    }
}
