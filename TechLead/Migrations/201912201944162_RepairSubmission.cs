namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepairSubmission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "Date");
        }
    }
}
