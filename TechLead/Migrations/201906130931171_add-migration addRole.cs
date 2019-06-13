namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationaddRole : DbMigration
    {
        public override void Up()
        {
            //Add some roles
            Sql("Insert into AspNetRoles (Id, Name) values (1, 'Administrator'), (2, 'User'), (3, 'Teacher'), (4, 'Student');");
        }
        
        public override void Down()
        {
            Sql("Delete from AspNetRoles where Id in (1,2,3,4)");
        }
    }
}
