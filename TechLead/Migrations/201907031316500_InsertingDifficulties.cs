namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertingDifficulties : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from Difficulties");
        }
        
        public override void Down()
        {
        }
    }
}
