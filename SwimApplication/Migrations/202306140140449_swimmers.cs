namespace SwimApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class swimmers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Swimmers",
                c => new
                    {
                        SwimmerID = c.Int(nullable: false, identity: true),
                        SwimmerName = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SwimmerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Swimmers");
        }
    }
}
