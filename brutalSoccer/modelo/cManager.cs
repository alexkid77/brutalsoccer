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
            var listaAgrupacion = m.Temporadas.Include("equipo").GroupBy(p => new { p.Division, p.temporada });
            foreach (var agrupacion in listaAgrupacion)
            {

                /*CREACION DE LA MATRIZ*/
                //todas las jornadas del equipo a una lista
                List<List<cResultadosJornada>> temporadas = new List<List<cResultadosJornada>>();
                foreach (var value in agrupacion)
                {
                    temporadas.Add((List<cResultadosJornada>)(value.jornadas));
                }


                cResultadosJornada[,] matriz = new cResultadosJornada[temporadas.Count, temporadas[0].Count];

                for (int i = 0; i < temporadas.Count; i++)
                    for (int j = 0; j < temporadas[i].Count; j++)
                    {
                        matriz[i, j] = temporadas[i][j];
                    }

                /*FIn de creacion de la matriz*/
                List<cResultadosJornada> resultadoGlobal = null;
                for (int i = 0; i < matriz.GetLength(1); i++)
                {
                    List<cResultadosJornada> listaJornada = new List<cResultadosJornada>();
                    for (int j = 0; j < matriz.GetLength(0); j++)
                    {
                        listaJornada.Add(matriz[j, i]);
                    }
                    List<cResultadosJornada> resultadoJornada = listaJornada.OrderBy(p => (p.numeroPartidosGanados*3 + p.numeroPartidosPerdidos*0 + p.numeroPartidosEmpadados*1)).ToList();
                    resultadoJornada.Reverse();
                    for (int k = 0; k < resultadoJornada.Count; k++)
                    {
                        resultadoJornada[k].posicion = k+1;
                    }

                    resultadoGlobal = listaJornada.OrderBy(p => p.posicion).ToList();
                   
                }

            }

            m.SaveChanges();

        }




    }

}
