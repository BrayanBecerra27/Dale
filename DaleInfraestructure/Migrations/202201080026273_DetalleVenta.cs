namespace DaleInfraestructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DetalleVenta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VENTA", "Producto_Id", "dbo.PRODUCTO");
            DropIndex("dbo.VENTA", new[] { "Producto_Id" });
            CreateTable(
                "dbo.DETALLEVENTA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Double(nullable: false),
                        ValorUnitario = c.Double(nullable: false),
                        ValorTotal = c.Double(nullable: false),
                        Producto_Id = c.Int(),
                        Venta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PRODUCTO", t => t.Producto_Id)
                .ForeignKey("dbo.VENTA", t => t.Venta_Id)
                .Index(t => t.Producto_Id)
                .Index(t => t.Venta_Id);
            
            AddColumn("dbo.VENTA", "Fecha", c => c.DateTime(nullable: false));
            DropColumn("dbo.VENTA", "Cantidad");
            DropColumn("dbo.VENTA", "ValorUnitario");
            DropColumn("dbo.VENTA", "Producto_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VENTA", "Producto_Id", c => c.Int());
            AddColumn("dbo.VENTA", "ValorUnitario", c => c.Double(nullable: false));
            AddColumn("dbo.VENTA", "Cantidad", c => c.Double(nullable: false));
            DropForeignKey("dbo.DETALLEVENTA", "Venta_Id", "dbo.VENTA");
            DropForeignKey("dbo.DETALLEVENTA", "Producto_Id", "dbo.PRODUCTO");
            DropIndex("dbo.DETALLEVENTA", new[] { "Venta_Id" });
            DropIndex("dbo.DETALLEVENTA", new[] { "Producto_Id" });
            DropColumn("dbo.VENTA", "Fecha");
            DropTable("dbo.DETALLEVENTA");
            CreateIndex("dbo.VENTA", "Producto_Id");
            AddForeignKey("dbo.VENTA", "Producto_Id", "dbo.PRODUCTO", "Id");
        }
    }
}
