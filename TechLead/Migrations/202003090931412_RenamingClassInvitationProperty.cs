namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamingClassInvitationProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "ClassInvitationCode", c => c.String());
            DropColumn("dbo.Classes", "ClassInvittionCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Classes", "ClassInvittionCode", c => c.String());
            DropColumn("dbo.Classes", "ClassInvitationCode");
        }
    }
}
