namespace OAuthenticationTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sssll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "DeptId", "dbo.Departments");
            DropPrimaryKey("dbo.Departments");
            DropPrimaryKey("dbo.Employees");
            AddColumn("dbo.Departments", "DeptId", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Employees", "EmpId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Departments", "DeptId");
            AddPrimaryKey("dbo.Employees", "EmpId");
            AddForeignKey("dbo.Employees", "DeptId", "dbo.Departments", "DeptId");
            DropColumn("dbo.Departments", "Id");
            DropColumn("dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Id", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Departments", "Id", c => c.Guid(nullable: false, identity: true));
            DropForeignKey("dbo.Employees", "DeptId", "dbo.Departments");
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Departments");
            DropColumn("dbo.Employees", "EmpId");
            DropColumn("dbo.Departments", "DeptId");
            AddPrimaryKey("dbo.Employees", "Id");
            AddPrimaryKey("dbo.Departments", "Id");
            AddForeignKey("dbo.Employees", "DeptId", "dbo.Departments", "Id");
        }
    }
}
