namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModelMembershipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "DurationInMonths", c => c.Byte(nullable: false));
            DropColumn("dbo.MembershipTypes", "DiscountInMonths");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MembershipTypes", "DiscountInMonths", c => c.Byte(nullable: false));
            DropColumn("dbo.MembershipTypes", "DurationInMonths");
        }
    }
}
