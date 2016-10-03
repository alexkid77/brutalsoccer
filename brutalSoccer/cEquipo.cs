using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cEquipo
    {
        public cManager manager { get; set; }
        public cEquipo(cManager manager)
        {
            this.manager = manager;
        }
        public string Nombre { get; set; }
        public List<cTemporada> temporadas = new List<cTemporada>();

        public void addPartido(cLinea linea, DateTime fechaIniTemporada, DateTime fechaFinTemporada)
        {
            bool valido = false;
            bool local = false;
            string equipoCargar = "";
            if (linea.local == this.Nombre)
            {
                valido = true;
                local = true;
                equipoCargar = linea.visitante;
            }
            else
            {
                valido = true;
                local = false;
                equipoCargar = linea.local;
            }




            string strTemporada = fechaIniTemporada.ToString("yy") + "/" + fechaFinTemporada.ToString("yy");

            List<cPartido> partidos = null;
            List<cPartido> partidos_ajenos = null;
            cEquipo equipo_ajeno = this.manager.equipos.Where(p => p.Nombre == equipoCargar).FirstOrDefault();

            cTemporada temporada = temporadas.Where(p => p.temporada == strTemporada).FirstOrDefault();
            cTemporada temporada_ajena = equipo_ajeno.temporadas.Where(p => p.temporada == strTemporada).FirstOrDefault();


            if (temporada == null)
            {
                temporada = new cTemporada(fechaIniTemporada, fechaFinTemporada);
                this.temporadas.Add(temporada);
            }
            else
            {
                int jj = 0;
                jj++;
            }

            if (temporada_ajena == null)
            {
                temporada_ajena = new cTemporada(fechaIniTemporada, fechaFinTemporada);
                equipo_ajeno.temporadas.Add(temporada_ajena);
            }

            partidos = temporada.partidos;
            partidos_ajenos = temporada_ajena.partidos;



            cPartido partido = new cPartido();
            if (local == true)
            {
                partido.Local = this;
                partido.Visitante = equipo_ajeno;
            }
            else
            {
                partido.Local = equipo_ajeno;
                partido.Visitante = this;
            }

            if (partidos.Count > 1)
            {
                int x = 0;
                x++;
            }

            partido.Fecha = linea.fecha;
            partido.Division = linea.division;
            partido.GolesPrimerTiempoLocal = linea.golesPrimerTiempoLocal;
            partido.GolesPrimerTiempoVisitante = linea.golesPrimerTiempoVisitante;
            partido.GolesTotalesLocal = linea.golesTotalesLocal;
            partido.GolesTotalesVisitante = linea.golesTotalesVisitante;
            partido.Resultado = linea.resultado;
            partido.ResultadoPrimerTiempo = linea.resultadoPrimerTiempo;

            partidos.Add(partido);
         //   partidos_ajenos.Add(partido);

        }


        public void procesaDatos()
        {

        }
    }
}
