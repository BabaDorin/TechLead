namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd : DbMigration
    {
        public override void Up()
        {
            Sql("DROP INDEX UserNameIndex ON aspnetusers");
            Sql("alter table aspnetusers drop column Username");
        }
        
        public override void Down()
        {
        }
    }
}
