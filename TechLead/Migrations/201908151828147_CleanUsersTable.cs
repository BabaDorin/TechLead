namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanUsersTable : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE AspNetUsers");
        }
        
        public override void Down()
        {
        }
    }
}
