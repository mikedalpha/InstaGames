namespace GroupProject.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_UserGameRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGameRatings",
                c => new
                    {
                        UserGameRatingsId = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserGameRatingsId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGameRatings", "GameId", "dbo.Games");
            DropForeignKey("dbo.UserGameRatings", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserGameRatings", new[] { "GameId" });
            DropIndex("dbo.UserGameRatings", new[] { "ApplicationUserId" });
            DropTable("dbo.UserGameRatings");
        }
    }
}
