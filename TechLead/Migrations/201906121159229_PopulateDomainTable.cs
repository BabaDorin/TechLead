namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDomainTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Difficulty (Name) Values ('Algorithms'), ('Data Structures'), ('Syntax');");
        }
        
        public override void Down()
        {
            Sql("Delete from Difficulty where Name ='Algoritms");
            Sql("Delete from Difficulty where Name ='Data Structures'");
            Sql("Delete from Difficulty where Name ='Syntax'");
        }
    }
}
