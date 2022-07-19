namespace Postermania.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brand = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        Secret = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Dimensions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Posters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PricePerCm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        type = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.Binary(),
                        IsAdmin = c.Boolean(nullable: false),
                        CreditCard_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CreditCards", t => t.CreditCard_ID)
                .Index(t => t.CreditCard_ID);
            
            CreateTable(
                "dbo.PosterDimensions",
                c => new
                    {
                        Poster_ID = c.Int(nullable: false),
                        Dimension_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Poster_ID, t.Dimension_ID })
                .ForeignKey("dbo.Posters", t => t.Poster_ID, cascadeDelete: true)
                .ForeignKey("dbo.Dimensions", t => t.Dimension_ID, cascadeDelete: true)
                .Index(t => t.Poster_ID)
                .Index(t => t.Dimension_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CreditCard_ID", "dbo.CreditCards");
            DropForeignKey("dbo.PosterDimensions", "Dimension_ID", "dbo.Dimensions");
            DropForeignKey("dbo.PosterDimensions", "Poster_ID", "dbo.Posters");
            DropIndex("dbo.PosterDimensions", new[] { "Dimension_ID" });
            DropIndex("dbo.PosterDimensions", new[] { "Poster_ID" });
            DropIndex("dbo.Users", new[] { "CreditCard_ID" });
            DropTable("dbo.PosterDimensions");
            DropTable("dbo.Users");
            DropTable("dbo.Posters");
            DropTable("dbo.Dimensions");
            DropTable("dbo.CreditCards");
        }
    }
}
