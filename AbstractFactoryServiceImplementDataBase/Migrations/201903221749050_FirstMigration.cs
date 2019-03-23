namespace AbstractFactoryServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ZBIId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.ZBIs", t => t.ZBIId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ZBIId);
            
            CreateTable(
                "dbo.ZBIs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZBIName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZBIMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZBIId = c.Int(nullable: false),
                        MaterialName = c.String(),
                        MaterialId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ZBIs", t => t.ZBIId, cascadeDelete: true)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.ZBIId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaterialName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StorageMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StorageMaterials", "MaterialId", "dbo.Storages");
            DropForeignKey("dbo.ZBIMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.StorageMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.ZBIMaterials", "ZBIId", "dbo.ZBIs");
            DropForeignKey("dbo.Orders", "ZBIId", "dbo.ZBIs");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.StorageMaterials", new[] { "MaterialId" });
            DropIndex("dbo.ZBIMaterials", new[] { "MaterialId" });
            DropIndex("dbo.ZBIMaterials", new[] { "ZBIId" });
            DropIndex("dbo.Orders", new[] { "ZBIId" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropTable("dbo.Storages");
            DropTable("dbo.StorageMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.ZBIMaterials");
            DropTable("dbo.ZBIs");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}
