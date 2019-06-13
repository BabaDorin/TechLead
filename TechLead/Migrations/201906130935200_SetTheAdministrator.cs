namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetTheAdministrator : DbMigration
    {
        public override void Up()
        {
            //set the bvd.dorin@gmail.com as administrator
            Sql("insert into AspNetUserRoles (userId, RoleId) values ('0f651bc9-d52f-46a3-9653-02758d3b111d',1);");
        }
        
        public override void Down()
        {
            Sql("delete from AspNetUserRoles where userId='0f651bc9-d52f-46a3-9653-02758d3b111d'");
        }
    }
}
