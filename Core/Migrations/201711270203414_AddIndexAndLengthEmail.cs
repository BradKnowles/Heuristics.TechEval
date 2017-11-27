namespace Heuristics.TechEval.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexAndLengthEmail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Member", "Email", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Member", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Member", new[] { "Email" });
            AlterColumn("dbo.Member", "Email", c => c.String(nullable: false));
        }
    }
}
