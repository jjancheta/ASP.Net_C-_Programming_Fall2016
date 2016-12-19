namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_newModel_ArtistMediaItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistMediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        StringId = c.String(nullable: false, maxLength: 100),
                        Content = c.Binary(),
                        ContentType = c.String(maxLength: 200),
                        Caption = c.String(nullable: false, maxLength: 100),
                        Artist_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .Index(t => t.Artist_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistMediaItems", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.ArtistMediaItems", new[] { "Artist_Id" });
            DropTable("dbo.ArtistMediaItems");
        }
    }
}
