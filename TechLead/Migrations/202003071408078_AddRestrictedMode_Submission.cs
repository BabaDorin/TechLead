namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRestrictedMode_Submission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "RestrictedMode", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "RestrictedMode");
        }
    }
}
