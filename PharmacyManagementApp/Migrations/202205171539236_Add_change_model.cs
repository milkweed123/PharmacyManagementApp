namespace PharmacyManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_change_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Changes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OldPrice = c.Double(nullable: false),
                        NewPrice = c.Double(nullable: false),
                        OldCount = c.Int(nullable: false),
                        NewCount = c.Int(nullable: false),
                        IsAutomaticChange = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Medicaments", "MNN", c => c.String(nullable: false));
            AlterColumn("dbo.Medicaments", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medicaments", "Name", c => c.String());
            AlterColumn("dbo.Medicaments", "MNN", c => c.String());
            DropTable("dbo.Changes");
        }
    }
}
