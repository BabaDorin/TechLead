namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sincron : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "AuthorID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "AuthorID");
        }
    }
}
