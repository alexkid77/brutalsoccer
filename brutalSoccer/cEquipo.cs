using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cEquipo
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual List<cTemporada> temporadas { get; set; }
        public cManager manager { get; set; }
        public cEquipo()
        {
        }

        public cEquipo(cManager manager)
        {
            this.manager = manager;
            this.temporadas= new List<cTemporada>();
        }
     

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

            ICollection<cPartido> partidos = null;
            ICollection<cPartido> partidos_ajenos = null;
            cEquipo equipo_ajeno = this.manager.equipos.Where(p => p.Nombre == equipoCargar).FirstOrDefault();

            cTemporada temporada = temporadas.Where(p => p.temporada == strTemporada).FirstOrDefault();
            cTemporada temporada_ajena = equipo_ajeno.temporadas.Where(p => p.temporada == strTemporada).FirstOrDefault();

          
            cPartido partido=null;
            if (temporada == null)
            {
                temporada = new cTemporada(this,linea.division,fechaIniTemporada, fechaFinTemporada);
                this.temporadas.Add(temporada);
               

            }
          

            if (temporada_ajena == null)
            {
                temporada_ajena = new cTemporada(equipo_ajeno,linea.division, fechaIniTemporada, fechaFinTemporada);
                
                equipo_ajeno.temporadas.Add(temporada_ajena);
            }

            partidos = temporada.partidos;
            partidos_ajenos = temporada_ajena.partidos;



         

            if (local == true)
            {
                partido = new cPartido(temporada, temporada_ajena);
                partido.Local = this;
                partido.Visitante = equipo_ajeno;
            }
            else
            {
                partido = new cPartido(temporada_ajena, temporada);
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
