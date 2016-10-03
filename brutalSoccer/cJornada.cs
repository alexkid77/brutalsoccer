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

        public int numeroGolesLocal { get; set; }
        public int numeroGolesVisitante { get; set; }
        public cTemporada temporada { get; set; }
    }

}
