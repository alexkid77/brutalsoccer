using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cManager
    {
        [Key]
        public int Id { get; set; }
        public int numeroTemporadas { get; set; }
        public virtual ICollection<cEquipo> equipos { get; set; }

        public cManager()
        {
            this.equipos = new List<cEquipo>();
        }

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


        }

        private void addEquipo(string nombre)
        {
            if (!((List<cEquipo>)equipos).Exists(p => p.Nombre == nombre))
            {

                cEquipo nuevoEquipo = new cEquipo(this);
                nuevoEquipo.Nombre = nombre;
                equipos.Add(nuevoEquipo);

            }
        }


        public void ProcesaJornadas()
        {
            /*Procesamos las estadisticas de cada joranda por cada equipo*/
            foreach (cEquipo equipo in this.equipos)
            {
                foreach (cTemporada temporada in equipo.temporadas)
                {
                    temporada.procesaTemporada();
                }
            }
            

        }

        public void ProcesaClasificacion()
        {
            cModelo m = new cModelo();
         var xx=   m.Temporadas.Include("equipo").GroupBy(p => new { p.Division, p.temporada });
            this.numeroTemporadas = this.equipos.Max(p => p.temporadas.Count);
            cEquipo equipoMax = this.equipos.Where(p => p.temporadas.Count == this.numeroTemporadas).FirstOrDefault();

            for (int i = 0; i < this.numeroTemporadas; i++)
            {
                List<List<cResultadosJornada>> matrixlista = new List<List<cResultadosJornada>>();
                foreach (cEquipo equipo in this.equipos)
                {
                    List<cTemporada> listaTemporada =(List<cTemporada>) equipo.temporadas;
                    matrixlista.Add((List<cResultadosJornada>)listaTemporada[i].jornadas);

                }
            }

        }




    }

}
