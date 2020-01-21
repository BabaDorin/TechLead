namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_MakeSourceCodePublic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Submissions", "MakeSourceCodePublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Submissions", "MakeSourceCodePublic");
        }
    }
}
