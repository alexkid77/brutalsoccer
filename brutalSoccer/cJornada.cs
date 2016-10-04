using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cResultadosJornada
    {
        [Key]
        public int Id { get; set; }

        public int posicion { get; set; }

        public int numeroPartidosJugados { get; set; }

        public int puntosAcumulados
        {
            get
            {
                return this.golesAcumuladosLocal + this.golesAcumuladosVisitante;
            }

        }
        public int golesAcumuladosVisitante { get; set; }
        public int golesAcumuladosLocal { get; set; }

        public int numeroPartidosGanados { get; set; }
        public int numeroPartidosPerdidos { get; set; }
        public int numeroPartidosEmpadados { get; set; }

        public int numeroGolesAfavorLocal { get; set; }
        public int numeroGolesAfavorVisitante { get; set; }

        public int numeroGolesEnContraLocal { get; set; }//goles que le han metido como local el equipo visitante
        public int numeroGolesEnContraVisitante { get; set; }//goles que le han metido como visitante el equipo local
        public cTemporada temporada { get; set; }
    }

}
