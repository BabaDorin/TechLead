namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSubmissionTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Submissions", "Time", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Submissions", "Time", c => c.DateTime(nullable: false));
        }
    }
}
