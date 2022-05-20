namespace PharmacyManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_Price_Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medicaments", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medicaments", "Price", c => c.Int(nullable: false));
        }
    }
}
