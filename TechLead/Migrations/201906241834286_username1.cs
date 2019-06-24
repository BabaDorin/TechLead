namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class username1 : DbMigration
    {
        public override void Up()
        {
            
            Sql("alter table AspNetUsers add UserName varchar(100) null");
        }

        public override void Down()
        {
            Sql("alter table Aspnetusers drop column UserName");
        }
    }
}
