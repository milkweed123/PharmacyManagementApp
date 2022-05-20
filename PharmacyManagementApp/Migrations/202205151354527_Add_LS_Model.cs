namespace PharmacyManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LS_Model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LS",
                c => new
                    {
                        MNN = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MNN);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LS");
        }
    }
}
