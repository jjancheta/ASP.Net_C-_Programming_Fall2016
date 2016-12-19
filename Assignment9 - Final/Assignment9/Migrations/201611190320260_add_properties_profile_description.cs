namespace Assignment9.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_properties_profile_description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Description", c => c.String());
            AddColumn("dbo.Artists", "Profile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artists", "Profile");
            DropColumn("dbo.Albums", "Description");
        }
    }
}
