using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
  
    public class cPartido
    {
        
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Division { get; set; }
        public cEquipo Local;
        public cEquipo Visitante;
        public int GolesPrimerTiempoLocal { get; set; }
        public int GolesPrimerTiempoVisitante { get; set; }
        public int GolesTotalesLocal { get; set; }
        public int GolesTotalesVisitante { get; set; }
        public string Resultado { get; set; }
        public string ResultadoPrimerTiempo { get; set; }
        public virtual cTemporada temporada { get; set; }
        public virtual cTemporada temporadaVisitante { get; set; }
        public cPartido(cTemporada temporada,cTemporada temporadaVisitante)
        {
            this.temporada = temporada;
            this.temporadaVisitante = temporadaVisitante;
            this.Local = temporada.equipo;
            this.Visitante = temporadaVisitante.equipo;
        }


    }

    public class cEntradasNeuro
    {
        public double golesNormVisitante;
        public double golesNormLocal;

        public double golesNormVisitantePrimeraParte;
        public double golesNormLocalSegundaParte;
        public double division;
        public cEntradasNeuro(cPartido partido)
        {
            if (partido.GolesTotalesVisitante != 0)
                this.golesNormVisitante = ((float)partido.GolesTotalesVisitante) / ((float)partido.GolesTotalesVisitante + (float)partido.GolesTotalesLocal);
            else
                this.golesNormVisitante = 0.5;


            if (partido.GolesPrimerTiempoVisitante != 0)
                this.golesNormVisitantePrimeraParte = ((float)partido.GolesPrimerTiempoVisitante) / ((float)partido.GolesPrimerTiempoVisitante + (float)partido.GolesPrimerTiempoLocal);
            else
                this.golesNormLocalSegundaParte = 0.5;

            if (partido.Division == "SP1")
                this.division = 0.0f;
            else
                this.division = 1.0f;
        }


    }

    public class cSalidaNeuro
    {
        public double empate = 0.0f;
        public double visitante = 0.0f;
        public double local = 0.0f;

        public cSalidaNeuro(cPartido partido)
        {
            switch (partido.Resultado)
            {
                case "D":
                    this.empate = 1.0f;
                    break;
                case "H":
                    this.local = 1.0f;
                    break;
                case "A":
                    this.visitante = 1.0f;
                    break;

            }


        }

    }
}
