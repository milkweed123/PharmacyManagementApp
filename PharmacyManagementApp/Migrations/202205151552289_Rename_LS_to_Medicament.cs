namespace PharmacyManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_LS_to_Medicament : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LS", newName: "Medicaments");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Medicaments", newName: "LS");
        }
    }
}
