namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanAspNetUsersTablex2 : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from AspNetUsers");
        }
        
        public override void Down()
        {
        }
    }
}
