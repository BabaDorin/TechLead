namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUselessProblems : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from Exercises where Id > 30");
        }
        
        public override void Down()
        {
        }
    }
}
