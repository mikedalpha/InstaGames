namespace GroupProject.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        Photo = c.String(nullable: false),
                        GameUrl = c.String(),
                        Trailer = c.String(),
                        ReleaseDate = c.DateTime(nullable: false, storeType: "date"),
                        IsEarlyAccess = c.Boolean(),
                        IsReleased = c.Boolean(nullable: false),
                        Tag = c.Byte(nullable: false),
                        PegiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Pegi", t => t.PegiId)
                .Index(t => t.PegiId);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        IsInstaGamesDev = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeveloperId);
            
            CreateTable(
                "dbo.Pegi",
                c => new
                    {
                        PegiId = c.Int(nullable: false, identity: true),
                        PegiPhoto = c.String(nullable: false),
                        PegiAge = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.PegiId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateOfBirth = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhotoUrl = c.String(),
                        SubscribePlan = c.Int(),
                        IsSubscribed = c.Boolean(nullable: false),
                        SubscriptionDay = c.DateTime(),
                        RegistrationDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Reply = c.String(),
                        SubmitDate = c.DateTime(nullable: false, storeType: "date"),
                        Answered = c.Boolean(nullable: false),
                        CreatorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.GamesCategories",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.CategoryId })
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.GamesDevelopers",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GameId, t.DeveloperId })
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Developers", t => t.DeveloperId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.DeveloperId);
            
            CreateTable(
                "dbo.SubscriberGames",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GameId, t.UserId })
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SubscriberGames", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubscriberGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.UserGameRatings", "GameId", "dbo.Games");
            DropForeignKey("dbo.UserGameRatings", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Games", "PegiId", "dbo.Pegi");
            DropForeignKey("dbo.GamesDevelopers", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.GamesDevelopers", "GameId", "dbo.Games");
            DropForeignKey("dbo.GamesCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.GamesCategories", "GameId", "dbo.Games");
            DropIndex("dbo.SubscriberGames", new[] { "UserId" });
            DropIndex("dbo.SubscriberGames", new[] { "GameId" });
            DropIndex("dbo.GamesDevelopers", new[] { "DeveloperId" });
            DropIndex("dbo.GamesDevelopers", new[] { "GameId" });
            DropIndex("dbo.GamesCategories", new[] { "CategoryId" });
            DropIndex("dbo.GamesCategories", new[] { "GameId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserGameRatings", new[] { "GameId" });
            DropIndex("dbo.UserGameRatings", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "CreatorId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Games", new[] { "PegiId" });
            DropTable("dbo.SubscriberGames");
            DropTable("dbo.GamesDevelopers");
            DropTable("dbo.GamesCategories");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserGameRatings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Pegi");
            DropTable("dbo.Developers");
            DropTable("dbo.Games");
            DropTable("dbo.Categories");
        }
    }
}
