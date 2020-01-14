namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClearAspNetRoles_Table : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from AspNetRoles");
        }
        
        public override void Down()
        {
        }
    }
}
