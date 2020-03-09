namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDbConetxtSync : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ClassExercises", newName: "ExerciseClasses");
            DropPrimaryKey("dbo.ExerciseClasses");
            AddPrimaryKey("dbo.ExerciseClasses", new[] { "Exercise_Id", "Class_ClassID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ExerciseClasses");
            AddPrimaryKey("dbo.ExerciseClasses", new[] { "Class_ClassID", "Exercise_Id" });
            RenameTable(name: "dbo.ExerciseClasses", newName: "ClassExercises");
        }
    }
}
