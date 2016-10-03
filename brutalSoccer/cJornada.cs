using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cJornada
    {
        [Key]
        public int Id { get; set; }
        public int posicion { get; set; }
        public int numeroPartidosJugados { get; set; }
        public double puntosAcumulados { get; set; }
        public double puntosPartidoAnterior { get; set; }
        public int numeroPartidosGanados { get; set; }
        public int numeroPartidosPerdidos { get; set; }
        public int numeroPartidosEmpadados { get; set; }
        public cTemporada temporada { get; set; }
    }

}
