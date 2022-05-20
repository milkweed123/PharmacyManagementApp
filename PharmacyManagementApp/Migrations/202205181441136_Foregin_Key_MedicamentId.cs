namespace PharmacyManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foregin_Key_MedicamentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Changes", "MedicamentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Changes", "MedicamentId");
            AddForeignKey("dbo.Changes", "MedicamentId", "dbo.Medicaments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Changes", "MedicamentId", "dbo.Medicaments");
            DropIndex("dbo.Changes", new[] { "MedicamentId" });
            DropColumn("dbo.Changes", "MedicamentId");
        }
    }
}
