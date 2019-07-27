namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubmissionTable_AddSourceCodeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "SourceCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "SourceCode");
        }
    }
}
