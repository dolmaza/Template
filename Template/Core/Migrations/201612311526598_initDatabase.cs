namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        Caption = c.String(nullable: false, maxLength: 200),
                        CaptionEng = c.String(maxLength: 200),
                        StringCode = c.String(maxLength: 200),
                        IntCode = c.Int(),
                        DecimalValue = c.Decimal(storeType: "money"),
                        Level = c.Int(),
                        Hierarchy = c.String(),
                        Code = c.Int(nullable: false),
                        SortIndex = c.Int(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentID = c.Int(),
                        Caption = c.String(nullable: false, maxLength: 200),
                        Url = c.String(maxLength: 200),
                        IconName = c.String(),
                        IsMenuItem = c.Boolean(nullable: false),
                        Code = c.String(nullable: false, maxLength: 50),
                        SortIndex = c.Int(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Permissions", t => t.ParentID)
                .Index(t => t.ParentID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false, maxLength: 200),
                        Code = c.Int(),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 200),
                        Password = c.String(nullable: false, maxLength: 200),
                        Firstname = c.String(maxLength: 200),
                        Lastname = c.String(maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID)
                .Index(t => t.Email, unique: true, name: "IX_EmailUnique");
            
            CreateTable(
                "dbo.RolePermissions",
                c => new
                    {
                        RoleID = c.Int(nullable: false),
                        PermissionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleID, t.PermissionID })
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.PermissionID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.PermissionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RolePermissions", "PermissionID", "dbo.Permissions");
            DropForeignKey("dbo.RolePermissions", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Permissions", "ParentID", "dbo.Permissions");
            DropForeignKey("dbo.Dictionaries", "ParentID", "dbo.Dictionaries");
            DropIndex("dbo.RolePermissions", new[] { "PermissionID" });
            DropIndex("dbo.RolePermissions", new[] { "RoleID" });
            DropIndex("dbo.Users", "IX_EmailUnique");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Permissions", new[] { "ParentID" });
            DropIndex("dbo.Dictionaries", new[] { "ParentID" });
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.Dictionaries");
        }
    }
}
