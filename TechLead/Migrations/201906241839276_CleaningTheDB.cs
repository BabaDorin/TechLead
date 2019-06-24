namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleaningTheDB : DbMigration
    {
        public override void Up()
        {
            Sql("delete from AspNetUsers");
        }
        
        public override void Down()
        {
        }
    }
}
