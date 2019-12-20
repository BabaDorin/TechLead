namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanDB : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from Exercises where ID != 16");
        }
        
        public override void Down()
        {
        }
    }
}
