namespace brutalSoccer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cEquipoes",
                c => new
                    {
                        Nombre = c.String(nullable: false, maxLength: 128),
                        manager_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Nombre)
                .ForeignKey("dbo.cManagers", t => t.manager_Id)
                .Index(t => t.manager_Id);
            
            CreateTable(
                "dbo.cManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        numeroTemporadas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.cTemporadas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        temporada = c.String(),
                        Division = c.String(),
                        fechaInicio = c.DateTime(nullable: false),
                        fechaFin = c.DateTime(nullable: false),
                        SinDatos = c.Boolean(nullable: false),
                        equipo_Nombre = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.cEquipoes", t => t.equipo_Nombre)
                .Index(t => t.equipo_Nombre);
            
            CreateTable(
                "dbo.cResultadosJornadas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        posicion = c.Int(nullable: false),
                        numeroPartidosJugados = c.Int(nullable: false),
                        golesAcumuladosVisitante = c.Int(nullable: false),
                        golesAcumuladosLocal = c.Int(nullable: false),
                        numeroPartidosGanados = c.Int(nullable: false),
                        numeroPartidosPerdidos = c.Int(nullable: false),
                        numeroPartidosEmpadados = c.Int(nullable: false),
                        numeroGolesLocal = c.Int(nullable: false),
                        numeroGolesVisitante = c.Int(nullable: false),
                        temporada_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.cTemporadas", t => t.temporada_Id)
                .Index(t => t.temporada_Id);
            
            CreateTable(
                "dbo.cPartidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Division = c.String(),
                        GolesPrimerTiempoLocal = c.Int(nullable: false),
                        GolesPrimerTiempoVisitante = c.Int(nullable: false),
                        GolesTotalesLocal = c.Int(nullable: false),
                        GolesTotalesVisitante = c.Int(nullable: false),
                        Resultado = c.String(),
                        ResultadoPrimerTiempo = c.String(),
                        temporada_Id = c.Int(),
                        temporadaVisitante_Id = c.Int(),
                        cTemporada_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.cTemporadas", t => t.temporada_Id)
                .ForeignKey("dbo.cTemporadas", t => t.temporadaVisitante_Id)
                .ForeignKey("dbo.cTemporadas", t => t.cTemporada_Id)
                .Index(t => t.temporada_Id)
                .Index(t => t.temporadaVisitante_Id)
                .Index(t => t.cTemporada_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cPartidoes", "cTemporada_Id", "dbo.cTemporadas");
            DropForeignKey("dbo.cPartidoes", "temporadaVisitante_Id", "dbo.cTemporadas");
            DropForeignKey("dbo.cPartidoes", "temporada_Id", "dbo.cTemporadas");
            DropForeignKey("dbo.cResultadosJornadas", "temporada_Id", "dbo.cTemporadas");
            DropForeignKey("dbo.cTemporadas", "equipo_Nombre", "dbo.cEquipoes");
            DropForeignKey("dbo.cEquipoes", "manager_Id", "dbo.cManagers");
            DropIndex("dbo.cPartidoes", new[] { "cTemporada_Id" });
            DropIndex("dbo.cPartidoes", new[] { "temporadaVisitante_Id" });
            DropIndex("dbo.cPartidoes", new[] { "temporada_Id" });
            DropIndex("dbo.cResultadosJornadas", new[] { "temporada_Id" });
            DropIndex("dbo.cTemporadas", new[] { "equipo_Nombre" });
            DropIndex("dbo.cEquipoes", new[] { "manager_Id" });
            DropTable("dbo.cPartidoes");
            DropTable("dbo.cResultadosJornadas");
            DropTable("dbo.cTemporadas");
            DropTable("dbo.cManagers");
            DropTable("dbo.cEquipoes");
        }
    }
}
