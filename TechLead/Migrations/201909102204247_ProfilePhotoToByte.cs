namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfilePhotoToByte : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePhotoPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePhotoPath", c => c.String());
        }
    }
}
