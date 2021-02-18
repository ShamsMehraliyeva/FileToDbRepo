namespace File_To_DB_Parser.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationInit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SettlementCategory = c.String(),
                        TransactionAmountCredit = c.Double(nullable: false),
                        ReconciliationAmnt = c.Double(nullable: false),
                        FeeAmountCredit = c.Double(nullable: false),
                        TransactionAmountDebit = c.Double(nullable: false),
                        ReconciliationAmntDebit = c.Double(nullable: false),
                        FeeAmountDebit = c.Double(nullable: false),
                        CountTotal = c.Double(nullable: false),
                        NetValue = c.Double(nullable: false),
                        TransactionID = c.Int(nullable: false),
                        CreateDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Transactions", t => t.TransactionID, cascadeDelete: true)
                .Index(t => t.TransactionID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FinancialInstitution = c.String(),
                        FXSettlementDate = c.String(),
                        ReconciliationFileID = c.String(),
                        TransactionCurrency = c.String(),
                        ReconciliationCurrency = c.String(),
                        CreateDatetime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionLines", "TransactionID", "dbo.Transactions");
            DropIndex("dbo.TransactionLines", new[] { "TransactionID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.TransactionLines");
        }
    }
}
