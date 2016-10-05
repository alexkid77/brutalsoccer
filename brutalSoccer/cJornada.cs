﻿using System;
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
        public string ResultadoPartido { get; set; }
        public int posicion { get; set; }

        public int numeroPartidosJugados { get; set; }

        public int puntosAcumulados
        {
            get
            {
                return this.golesAcumuladosAfavorLocal + this.golesAcumuladosAfavorVisitante;
            }

        }
        public int golesAcumuladosAfavorVisitante { get; set; }
        public int golesAcumuladosAfavorLocal { get; set; }

        public int golesAcumuladosEnContraVisitante { get; set; }
        public int golesAcumuladosEnContraLocal { get; set; }

        public int numeroPartidosGanados { get; set; }
        public int numeroPartidosPerdidos { get; set; }
        public int numeroPartidosEmpadados { get; set; }

        public int numeroGolesAfavorLocal { get; set; }
        public int numeroGolesAfavorVisitante { get; set; }

        public int numeroGolesEnContraLocal { get; set; }//goles que le han metido como local el equipo visitante
        public int numeroGolesEnContraVisitante { get; set; }//goles que le han metido como visitante el equipo local

        public int ptosJornada { get; set; } //los ptos de la jornada actual
        public int diferenciaGolesJornada { get; set; }//la diferencia de goles en la jornada actual

        public int posicionJornadaActual { get; set; }
        public int ptosStandar
        {
            get
            {
                return this.numeroPartidosGanados * 3 + this.numeroPartidosPerdidos * 0 + this.numeroPartidosEmpadados * 1;
            }
        }

        public int diferenciaGoles
        {
            get {
                return (this.golesAcumuladosAfavorLocal + this.golesAcumuladosAfavorVisitante) - (this.golesAcumuladosEnContraLocal + this.golesAcumuladosEnContraVisitante);
            }
        }

        public cTemporada temporada { get; set; }
    }

}
