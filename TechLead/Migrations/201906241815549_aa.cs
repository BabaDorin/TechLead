namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            Sql("alter table Aspnetusers drop column UserName");
        }

        public override void Down()
        {
            Sql("insert into aspnetusers UserName varchar(100) null");
        }
    }
}
