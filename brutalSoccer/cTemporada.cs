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
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public virtual ICollection<cPartido> partidos { get; set; }
        public virtual ICollection<cResultadosJornada> jornadas { get; set; }
        public cEquipo equipo { get; set; }

        public cTemporada(cEquipo e,DateTime fechaInicio, DateTime fechaFin)
        {
            this.equipo = e;
            string strTemporada = fechaInicio.ToString("yy") + "/" + fechaFin.ToString("yy");
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.partidos = new List<cPartido>();
            this.jornadas = new List<cResultadosJornada>();
            this.temporada = strTemporada;
        }

        public void procesaTemporada()
        {
            int npartidos = 0;
            cResultadosJornada acumulado = new cResultadosJornada();
         
            foreach (cPartido p in this.partidos)
            {
                cResultadosJornada resultado = new cResultadosJornada();

                if (p.temporada== this)//es local
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
                    

                    resultado.numeroGolesLocal = p.GolesTotalesLocal;
                    acumulado.golesAcumuladosLocal += p.GolesTotalesLocal;
                   
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

                    resultado.numeroGolesVisitante = p.GolesTotalesVisitante;
                    acumulado.golesAcumuladosVisitante += p.GolesTotalesVisitante;
                   
                }
             

                npartidos++;
                resultado.numeroPartidosJugados = npartidos;
                resultado.numeroPartidosGanados = acumulado.numeroPartidosGanados;
                resultado.numeroPartidosPerdidos = acumulado.numeroPartidosPerdidos;
                resultado.numeroPartidosEmpadados = acumulado.numeroPartidosEmpadados;

                resultado.golesAcumuladosLocal = acumulado.golesAcumuladosLocal;
                resultado.golesAcumuladosVisitante = acumulado.golesAcumuladosVisitante;

                resultado.temporada = this;
                this.jornadas.Add(resultado);
            }

           
        }

    }


   



}

