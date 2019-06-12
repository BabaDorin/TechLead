namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDomainTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Domains (Name) Values ('Algorithms'), ('Data Structures'), ('Syntax');");
        }
        
        public override void Down()
        {
            Sql("Delete from Domains where Name ='Algoritms");
            Sql("Delete from Domains where Name ='Data Structures'");
            Sql("Delete from Domains where Name ='Syntax'");
        }
    }
}
