namespace GroupProject.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        SubmitDate = c.DateTime(nullable: false, storeType: "date"),
                        Creator_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id, cascadeDelete: true)
                .Index(t => t.Creator_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "Creator_Id" });
            DropTable("dbo.Messages");
        }
    }
}
