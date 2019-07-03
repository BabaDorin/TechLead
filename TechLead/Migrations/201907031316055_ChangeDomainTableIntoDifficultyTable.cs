namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDomainTableIntoDifficultyTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Domains", newName: "Difficulties");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Difficulties", newName: "Domains");
        }
    }
}
