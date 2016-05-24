using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cLinea
    {
        public string division { get; set; }
        public DateTime fecha { get; set; }
        public string local { get; set; }
        public string visitante { get; set; }

        public int golesTotalesLocal { get; set; }
        public int golesTotalesVisitante { get; set; }
        public string resultado { get; set; }

        public int golesPrimerTiempoLocal { get; set; }
        public int golesPrimerTiempoVisitante { get; set; }
        public string resultadoPrimerTiempo { get; set; }

        public cLinea(CsvReader csv)
        {
            this.division = csv.GetField<string>("Div");
         string tempdate=   csv.GetField<string>("Date");
            this.fecha = DateTime.ParseExact(tempdate, "dd/MM/yy", CultureInfo.InvariantCulture);
            this.local = csv.GetField<string>("HomeTeam");
            this.visitante = csv.GetField<string>("AwayTeam");
            this.golesTotalesLocal = csv.GetField<int>("FTHG");
            this.golesTotalesVisitante = csv.GetField<int>("FTAG");
            this.resultado = csv.GetField<string>("FTR");

            this.golesPrimerTiempoLocal = csv.GetField<int>("HTHG");
            this.golesPrimerTiempoVisitante = csv.GetField<int>("HTAG");
            this.resultadoPrimerTiempo = csv.GetField<string>("HTR");
        }
    }
}
