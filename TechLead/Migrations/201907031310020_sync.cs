namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sync : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exercises", "Domain_Id", "dbo.Difficulty");
            DropIndex("dbo.Exercises", new[] { "Domain_Id" });
            DropColumn("dbo.Exercises", "Domain_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "Domain_Id", c => c.Int());
            CreateIndex("dbo.Exercises", "Domain_Id");
            AddForeignKey("dbo.Exercises", "Domain_Id", "dbo.Difficulty", "Id");
        }
    }
}
