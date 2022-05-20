namespace PharmacyManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_PrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Medicaments");
            AddColumn("dbo.Medicaments", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Medicaments", "MNN", c => c.String());
            AddPrimaryKey("dbo.Medicaments", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Medicaments");
            AlterColumn("dbo.Medicaments", "MNN", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Medicaments", "Id");
            AddPrimaryKey("dbo.Medicaments", "MNN");
        }
    }
}
