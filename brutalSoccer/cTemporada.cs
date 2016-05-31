using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cManager
    {

        public List<cEquipo> equipos = new List<cEquipo>();
        public void procesaNombreEquipos(cLinea linea)
        {
            this.addEquipo(linea.local);
            this.addEquipo(linea.visitante);

        }

        public void procesaTemporada(List<cLinea> lineas)
        {

            foreach (cLinea l in lineas)
            {
                this.procesaNombreEquipos(l);
            }

            DateTime fechaMin = lineas.Min(p => p.fecha);
            DateTime fechaMax = lineas.Max(p => p.fecha);


            foreach (cLinea l in lineas)
            {

                cEquipo local = this.equipos.Where(p => p.Nombre == l.local).FirstOrDefault();
                cEquipo visitante = this.equipos.Where(p => p.Nombre == l.visitante).FirstOrDefault();
                local.addPartido(l, fechaMin, fechaMax);
                visitante.addPartido(l, fechaMin, fechaMax);
            }

            //cTemporada temporada = new cTemporada(fechaMin, fechaMax);
        }

        private void addEquipo(string nombre)
        {
            if (!equipos.Exists(p => p.Nombre == nombre))
            {
                cEquipo nuevoEquipo = new cEquipo(this);
                nuevoEquipo.Nombre = nombre;
                equipos.Add(nuevoEquipo);
            }
        }





    }


    public class cTemporada
    {

        public string temporada { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public List<cPartido> partidos { get; set; }

        public cTemporada(DateTime fechaInicio, DateTime fechaFin)
        {
            string strTemporada = fechaInicio.ToString("yy") + "/" + fechaFin.ToString("yy");
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.partidos = new List<cPartido>();
            this.temporada = strTemporada;
        }


    }

    public class cEquipo
    {
        cManager manager;
        public cEquipo(cManager manager)
        {
            this.manager = manager;
        }
        public string Nombre { get; set; }
        List<cTemporada> temporadas = new List<cTemporada>();

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
           
      
            partidos.Add(partido);
            partidos_ajenos.Add(partido);
          
        }
    }

    public class cPartido
    {
        public DateTime Fecha { get; set; }
        public string Division { get; set; }
        public cEquipo Local { get; set; }
        public cEquipo Visitante { get; set; }
        public int GolesPrimerTiempoLocal { get; set; }
        public int GolesPrimerTiempoVisitante { get; set; }
        public int GolesTotalesLocal { get; set; } 
        public int GolesTotalesVisitante { get; set; }
        public string Resultado { get; set; }
        public string ResultadoPrimerTiempo { get; set; }
    }


}
