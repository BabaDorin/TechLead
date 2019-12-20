namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanDBx2 : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from Exercises");
        }
        
        public override void Down()
        {
        }
    }
}
