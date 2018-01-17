namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDataInMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET Name = 'Pay as you go' WHERE Id = '1'");
            Sql("UPDATE MembershipTypes SET Name = 'monthly' WHERE Id = '2'");
            Sql("UPDATE MembershipTypes SET Name = 'mothly' WHERE Id = '3'");
            Sql("UPDATE MembershipTypes SET Name = 'Pay as you go' WHERE Id = '4'");
        }
        
        public override void Down()
        {
        }
    }
}
