using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cManager
    {

        List<cEquipo> equipos = new List<cEquipo>();
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

            }

            //cTemporada temporada = new cTemporada(fechaMin, fechaMax);
        }

        private void addEquipo(string nombre)
        {
            if (!equipos.Exists(p => p.Nombre == nombre))
            {
                cEquipo nuevoEquipo = new cEquipo();
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
        }


    }

    public class cEquipo
    {
        public string Nombre { get; set; }
        List<cTemporada> temporadas = new List<cTemporada>();

        public void addPartido(cLinea linea, DateTime fechaIniTemporada, DateTime fechaFinTemporada)
        {
            bool valido = false;
            bool local = false;

            if (linea.local == this.Nombre)
            {
                valido = true;
                local = true;
            }

            if (linea.visitante == this.Nombre)
            {
                valido = true;
                local = false;
            }

            if (valido == false)
                return;


            List<cPartido> partidos = null;
            cTemporada temporada = temporadas.Where(p => p.fechaInicio >= linea.fecha && p.fechaFin <= linea.fecha).FirstOrDefault();
            if (temporada == null)
            {
                temporada = new cTemporada(fechaIniTemporada, fechaFinTemporada);
            }
            partidos = temporada.partidos;


        }
    }

    public class cPartido
    {
        public string Division { get; set; }
        public cEquipo Local { get; set; }
        public cEquipo Visitante { get; set; }

    }


}
