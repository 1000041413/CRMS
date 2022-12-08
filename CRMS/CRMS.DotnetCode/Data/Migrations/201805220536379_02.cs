namespace Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Bookings", name: "EmpID", newName: "UserId");
            RenameIndex(table: "dbo.Bookings", name: "IX_EmpID", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Bookings", name: "IX_UserId", newName: "IX_EmpID");
            RenameColumn(table: "dbo.Bookings", name: "UserId", newName: "EmpID");
        }
    }
}
