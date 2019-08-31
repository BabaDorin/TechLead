namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProprieties_Job_FirstRegistration_Points : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Job", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstRegistration", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "TotalPoints", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TotalPoints");
            DropColumn("dbo.AspNetUsers", "FirstRegistration");
            DropColumn("dbo.AspNetUsers", "Job");
        }
    }
}
