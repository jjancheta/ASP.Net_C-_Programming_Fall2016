namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_FileName_MediaItem_Track : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MediaItems", "FileName", c => c.String());
            AddColumn("dbo.Tracks", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "FileName");
            DropColumn("dbo.MediaItems", "FileName");
        }
    }
}
