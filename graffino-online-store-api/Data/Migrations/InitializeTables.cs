using FluentMigrator;

namespace graffino_online_store_api.Data.Migrations;

[Migration(114052024)]
public class InitializeTables : Migration
{
    public override void Up()
    {
        CreateProductTables();
    }

    private void CreateProductTables()
    {
        Create.Table("products")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("name").AsString(256).NotNullable()
            .WithColumn("price").AsDouble().NotNullable()
            .WithColumn("category").AsInt32().NotNullable()
            .WithColumn("dateAdded").AsDateTime().NotNullable();

        Create.Table("clothing")
            .WithColumn("id").AsInt32().PrimaryKey().ForeignKey("products", "id")
            .WithColumn("color").AsString(64).NotNullable()
            .WithColumn("style").AsString(64).NotNullable()
            .WithColumn("size").AsString(32).NotNullable();

        Create.Table("televisions")
            .WithColumn("id").AsInt32().PrimaryKey().ForeignKey("products", "id")
            .WithColumn("diameter").AsString(64).NotNullable()
            .WithColumn("resolution").AsString(64).NotNullable();
    }
    
    public override void Down()
    {
        Delete.Table("televisions");
        Delete.Table("clothing");
        Delete.Table("products");
    }
}