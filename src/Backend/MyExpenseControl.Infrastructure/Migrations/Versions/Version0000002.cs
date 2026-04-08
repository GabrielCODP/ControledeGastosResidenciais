using FluentMigrator;

namespace MyExpenseControl.Infrastructure.Migrations.Versions
{
    [Migration(DatabaseVersions.TABLE_TRANSACTION_CATEGORY, "Create table to save the transaction's information")]
    public class Version0000002 : VersionBase
    {
        public override void Up()
        {

            CreateTable("Categories")
                .WithColumn("Description").AsString(400).NotNullable()
                .WithColumn("Purpose").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Categories_User_Id", "Users", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            CreateTable("FinancialTransactions")
                .WithColumn("Description").AsString(400).NotNullable()
                .WithColumn("Amount").AsDecimal(18, 2).NotNullable()
                .WithColumn("TransactionType").AsInt32().NotNullable()
                .WithColumn("CategoryId").AsInt64().NotNullable().ForeignKey("FK_FinancialTransactions_Categories_Id", "Categories", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_FinancialTransactions_User_Id", "Users", "Id")
                .OnDelete(System.Data.Rule.Cascade);

        }
    }
}
