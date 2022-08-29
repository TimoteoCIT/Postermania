namespace Postermania.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddescriptiontoposter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posters", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posters", "Description");
        }
    }
}
