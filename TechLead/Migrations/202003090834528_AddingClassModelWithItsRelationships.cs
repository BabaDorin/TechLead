namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingClassModelWithItsRelationships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassID = c.Int(nullable: false, identity: true),
                        ClassName = c.String(nullable: false),
                        ClassInfo = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ClassCreatorID = c.String(),
                        ClassInvittionCode = c.String(),
                    })
                .PrimaryKey(t => t.ClassID);
            CreateTable(
                "dbo.ClassExercises",
                c => new
                    {
                        Class_ClassID = c.Int(nullable: false),
                        Exercise_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Class_ClassID, t.Exercise_Id })
                .ForeignKey("dbo.Classes", t => t.Class_ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id, cascadeDelete: true)
                .Index(t => t.Class_ClassID)
                .Index(t => t.Exercise_Id);
            
            CreateTable(
                "dbo.ApplicationUserClasses",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Class_ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Class_ClassID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_ClassID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Class_ClassID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserClasses", "Class_ClassID", "dbo.Classes");
            DropForeignKey("dbo.ApplicationUserClasses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ClassExercises", "Exercise_Id", "dbo.Exercises");
            DropForeignKey("dbo.ClassExercises", "Class_ClassID", "dbo.Classes");
            DropIndex("dbo.ApplicationUserClasses", new[] { "Class_ClassID" });
            DropIndex("dbo.ApplicationUserClasses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ClassExercises", new[] { "Exercise_Id" });
            DropIndex("dbo.ClassExercises", new[] { "Class_ClassID" });
            DropTable("dbo.ApplicationUserClasses");
            DropTable("dbo.ClassExercises");
            DropTable("dbo.Classes");
        }
    }
}
