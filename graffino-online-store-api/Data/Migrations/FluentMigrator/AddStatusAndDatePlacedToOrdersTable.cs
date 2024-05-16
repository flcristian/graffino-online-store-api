
using FluentMigrator;

namespace graffino_online_store_api.Data.Migrations;

[Migration(316052024)]
public class AddStatusAndDatePlacedToOrdersTable : Migration
{
    public override void Up()
    {
        Alter.Table("Orders")
            .AddColumn("status").AsInt32().NotNullable().WithDefaultValue(0)
            .AddColumn("lastDateUpdated").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete.Column("status").FromTable("Orders");
        Delete.Column("lastDateUpdated").FromTable("Orders");
    }
}