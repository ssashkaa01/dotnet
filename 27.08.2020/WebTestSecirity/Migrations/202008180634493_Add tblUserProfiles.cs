namespace WebTestSecirity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtblUserProfiles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblUserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 255),
                        FirstName = c.String(nullable: false, maxLength: 255),
                        Patronymic = c.String(nullable: false, maxLength: 255),
                        Gender = c.Int(nullable: false),
                        Hobby = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblUserProfiles", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.tblUserProfiles", new[] { "Id" });
            DropTable("dbo.tblUserProfiles");
        }
    }
}
