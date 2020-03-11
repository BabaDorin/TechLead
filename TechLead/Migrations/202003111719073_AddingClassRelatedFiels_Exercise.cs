namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClassRelatedFiels_Exercise : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "MotherClassID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "MotherClassID");
        }
    }
}
