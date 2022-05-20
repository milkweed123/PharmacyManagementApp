namespace PharmacyManagementApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ChangeDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Changes", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Changes", "Date");
        }
    }
}
