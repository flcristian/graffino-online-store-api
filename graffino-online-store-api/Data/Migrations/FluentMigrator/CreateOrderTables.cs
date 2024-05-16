using FluentMigrator;

namespace graffino_online_store_api.Data.Migrations;

[Migration(216052024)]
public class CreateOrderTables : Migration
{
    public override void Up()
    {
        CreateOrderTable();
        CreateOrderDetailsTable();
        CreateIndexes();
        CreateForeignKeys();
    }

    private void CreateOrderTable()
    {
        Create.Table("orders")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("customerId").AsGuid().NotNullable();
    }

    private void CreateOrderDetailsTable()
    {
        Create.Table("orderDetails")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("orderId").AsInt32().NotNullable()
            .WithColumn("productId").AsInt32().NotNullable()
            .WithColumn("count").AsInt32().NotNullable();
    }

    private void CreateIndexes()
    {
        Create.Index("IX_OrderDetails_OrderId")
            .OnTable("OrderDetails")
            .OnColumn("OrderId")
            .Ascending()
            .WithOptions()
            .NonClustered();
        
        Create.Index("IX_OrderDetails_ProductId")
            .OnTable("OrderDetails")
            .OnColumn("ProductId")
            .Ascending()
            .WithOptions()
            .NonClustered();
        
        Create.Index("IX_Orders_CustomerId")
            .OnTable("Orders")
            .OnColumn("CustomerId")
            .Ascending()
            .WithOptions()
            .NonClustered();
    }

    private void CreateForeignKeys()
    {
        Create.ForeignKey("FK_OrderDetails_Order")
            .FromTable("OrderDetails")
            .ForeignColumn("OrderId")
            .ToTable("Orders")
            .PrimaryColumn("Id");
        
        Create.ForeignKey("FK_OrderDetails_Product")
            .FromTable("OrderDetails")
            .ForeignColumn("ProductId")
            .ToTable("Products")
            .PrimaryColumn("Id");
        
        Create.ForeignKey("FK_Orders_Customer")
            .FromTable("Orders")
            .ForeignColumn("CustomerId")
            .ToTable("AspNetUsers")
            .PrimaryColumn("Id");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_OrderDetails_Order").OnTable("OrderDetails");
        Delete.ForeignKey("FK_OrderDetails_Product").OnTable("OrderDetails");
        Delete.ForeignKey("FK_Orders_Customer").OnTable("Orders");

        Delete.Index("IX_OrderDetails_OrderId").OnTable("OrderDetails");
        Delete.Index("IX_OrderDetails_ProductId").OnTable("OrderDetails");
        Delete.Index("IX_Orders_CustomerId").OnTable("Orders");

        Delete.Table("OrderDetails");
        Delete.Table("Orders");
    }
}