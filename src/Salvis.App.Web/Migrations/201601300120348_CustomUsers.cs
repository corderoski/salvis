namespace Salvis.App.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(maxLength: 200));
            AddColumn("dbo.AspNetUsers", "Enable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Enable");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}
