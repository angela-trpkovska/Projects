namespace AmsmProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AmsmInfoPP2",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        hour = c.Int(nullable: false),
                        place = c.String(),
                        candidatesPP2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Candidate",
                c => new
                    {
                        EMBG = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(nullable: false, maxLength: 20),
                        parentName = c.String(nullable: false, maxLength: 20),
                        lastName = c.String(nullable: false, maxLength: 30),
                        category = c.String(nullable: false),
                        drivingSchool = c.String(nullable: false, maxLength: 30),
                        instructor = c.String(nullable: false, maxLength: 30),
                        registered = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EMBG);
            
            CreateTable(
                "dbo.PracticalPart1",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EMBG = c.String(),
                        date = c.DateTime(nullable: false),
                        passed = c.Boolean(nullable: false),
                        payed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PracticalPart2",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EMBG = c.String(),
                        date = c.DateTime(nullable: false),
                        place = c.String(),
                        hour = c.Int(nullable: false),
                        passed = c.Boolean(nullable: false),
                        payed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TheoryPart",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EMBG = c.String(),
                        date = c.DateTime(nullable: false),
                        hour = c.Int(nullable: false),
                        passed = c.Boolean(nullable: false),
                        payed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CreditCardInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EMBG = c.String(),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        cardNumber = c.String(nullable: false),
                        expiresDate = c.String(nullable: false),
                        code = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EditUserViewModel",
                c => new
                    {
                        EMBG = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 30),
                        ConfirmPassword = c.String(),
                        firstName = c.String(nullable: false),
                        parentName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        category = c.String(nullable: false),
                        drivingSchool = c.String(nullable: false),
                        instructor = c.String(nullable: false),
                        mail = c.String(),
                        code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EMBG);
            
            CreateTable(
                "dbo.AmsmInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        hour = c.Int(nullable: false),
                        candidatesTP = c.Int(nullable: false),
                        candidatesPP1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AmsmInfo");
            DropTable("dbo.EditUserViewModel");
            DropTable("dbo.CreditCardInfo");
            DropTable("dbo.TheoryPart");
            DropTable("dbo.PracticalPart2");
            DropTable("dbo.PracticalPart1");
            DropTable("dbo.Candidate");
            DropTable("dbo.AmsmInfoPP2");
        }
    }
}
