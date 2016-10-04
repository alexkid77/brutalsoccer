using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{


    public class cTemporada
    {
        [Key]
        public int Id { get; set; }
        public string temporada { get; set; }
        public string Division { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public virtual List<cPartido> partidos { get; set; }
        public virtual List<cResultadosJornada> jornadas { get; set; }
        public virtual cEquipo equipo { get; set; }
        public bool SinDatos { get; set; }
        public int equipoId { get; set; }
        public cTemporada(cEquipo e, string division, DateTime fechaInicio, DateTime fechaFin)
        {
            this.SinDatos = false;
            this.Division = division;
            this.equipo = e;
            string strTemporada = fechaInicio.ToString("yy") + "/" + fechaFin.ToString("yy");
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.partidos = new List<cPartido>();
            this.jornadas = new List<cResultadosJornada>();
            this.temporada = strTemporada;
        }
        public cTemporada()
        {

        }
        public void procesaTemporada()
        {
            int npartidos = 0;
            cResultadosJornada acumulado = new cResultadosJornada();

            foreach (cPartido p in this.partidos)
            {
                cResultadosJornada resultado = new cResultadosJornada();

                if (p.temporada == this)//es local
                {
                    switch (p.Resultado)
                    {
                        case "H":
                            acumulado.numeroPartidosGanados++;
                            break;
                        case "D":
                            acumulado.numeroPartidosEmpadados++;
                            break;
                        case "A":
                            acumulado.numeroPartidosPerdidos++;
                            break;
                    }

                    resultado.numeroGolesEnContraLocal = p.GolesTotalesVisitante;
                    resultado.numeroGolesAfavorLocal = p.GolesTotalesLocal;
                    acumulado.golesAcumuladosAfavorLocal += p.GolesTotalesLocal;
                    acumulado.golesAcumuladosEnContraLocal += p.GolesTotalesVisitante;


                }
                else//es visitante
                {
                    switch (p.Resultado)
                    {
                        case "H":
                            acumulado.numeroPartidosPerdidos++;

                            break;
                        case "D":
                            acumulado.numeroPartidosEmpadados++;
                            break;
                        case "A":
                            acumulado.numeroPartidosGanados++;
                            break;
                    }
                    resultado.numeroGolesEnContraVisitante = p.GolesTotalesLocal;
                    resultado.numeroGolesAfavorVisitante = p.GolesTotalesVisitante;
                    acumulado.golesAcumuladosAfavorVisitante += p.GolesTotalesVisitante;
                    acumulado.golesAcumuladosEnContraVisitante += p.GolesTotalesLocal;
                }


                npartidos++;
                resultado.numeroPartidosJugados = npartidos;
                resultado.numeroPartidosGanados = acumulado.numeroPartidosGanados;
                resultado.numeroPartidosPerdidos = acumulado.numeroPartidosPerdidos;
                resultado.numeroPartidosEmpadados = acumulado.numeroPartidosEmpadados;

                resultado.golesAcumuladosAfavorLocal = acumulado.golesAcumuladosAfavorLocal;
                resultado.golesAcumuladosAfavorVisitante = acumulado.golesAcumuladosAfavorVisitante;

                resultado.golesAcumuladosEnContraLocal = acumulado.golesAcumuladosEnContraLocal;
                resultado.golesAcumuladosEnContraVisitante = acumulado.golesAcumuladosEnContraVisitante;
              
               
                    resultado.temporada = this;
                this.jornadas.Add(resultado);
            }


        }

    }






}

