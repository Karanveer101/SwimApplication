namespace SwimApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sessions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        SessionID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Distance = c.Int(nullable: false),
                        Duration = c.Time(nullable: false, precision: 7),
                        StrokeType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SessionID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sessions");
        }
    }
}
