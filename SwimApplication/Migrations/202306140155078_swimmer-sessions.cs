namespace SwimApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class swimmersessions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "SwimmerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sessions", "SwimmerId");
            AddForeignKey("dbo.Sessions", "SwimmerId", "dbo.Swimmer", "SwimmerID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "SwimmerId", "dbo.Swimmer");
            DropIndex("dbo.Sessions", new[] { "SwimmerId" });
            DropColumn("dbo.Sessions", "SwimmerId");
        }
    }
}
