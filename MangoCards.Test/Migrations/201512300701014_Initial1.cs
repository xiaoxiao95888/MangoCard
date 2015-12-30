namespace MangoCards.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CardDemoes", "Thumbnail", c => c.String());
            DropColumn("dbo.CardDemoes", "ThumbnailUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CardDemoes", "ThumbnailUrl", c => c.String());
            DropColumn("dbo.CardDemoes", "Thumbnail");
        }
    }
}
