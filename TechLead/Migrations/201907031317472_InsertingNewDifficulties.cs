namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertingNewDifficulties : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into Difficulties " +
                "(Name)" +
                "values" +
                "('Beginner')," +
                "('Intermediate')," +
                "('Advanced')");
        }
        
        public override void Down()
        {
            Sql("Delete from Difficulties where Name='Beginner'");
            Sql("Delete from Difficulties where Name='Intermediate'");
            Sql("Delete from Difficulties where Name='Advanced'");
        }
    }
}
