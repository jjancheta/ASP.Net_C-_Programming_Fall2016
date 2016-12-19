namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_ArtistMediaItem_MediaItem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ArtistMediaItems", newName: "MediaItems");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MediaItems", newName: "ArtistMediaItems");
        }
    }
}
