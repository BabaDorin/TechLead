namespace TechLead.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProblemTrashCan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProblemsTrashCan",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Points = c.Int(nullable: false),
                        SubmissionsAbove10Points = c.Int(nullable: false),
                        SubmissionsUnder10Points = c.Int(nullable: false),
                        Author = c.String(),
                        AuthorID = c.String(),
                        DifficultyId = c.Int(nullable: false),
                        Condition = c.String(nullable: false),
                        InputFormat = c.String(nullable: false),
                        OutputFormat = c.String(nullable: false),
                        Constraints = c.String(nullable: false),
                        Input1 = c.String(nullable: false),
                        Explanation1 = c.String(nullable: false),
                        Input2 = c.String(),
                        Explanation2 = c.String(),
                        Input3 = c.String(),
                        Explanation3 = c.String(),
                        Input4 = c.String(),
                        Explanation4 = c.String(),
                        Input5 = c.String(),
                        Explanation5 = c.String(),
                        Output1 = c.String(nullable: false),
                        Output2 = c.String(),
                        Output3 = c.String(),
                        Output4 = c.String(),
                        Output5 = c.String(),
                        InputColection = c.String(),
                        OutputColection = c.String(),
                        NumberOfTests = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProblemsTrashCan");
        }
    }
}
