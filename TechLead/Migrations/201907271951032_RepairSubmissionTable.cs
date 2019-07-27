namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RepairSubmissionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "Points", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "Points");
        }
    }
}
