namespace MangoCards.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CardDemoes", "Title", c => c.String());
            AddColumn("dbo.CardDemoes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CardDemoes", "Description");
            DropColumn("dbo.CardDemoes", "Title");
        }
    }
}
