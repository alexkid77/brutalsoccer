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
        public List<cPartido> partidos { get; set; }
        public List<cJornada> jornadas { get; set; }
        public cTemporada(DateTime fechaInicio, DateTime fechaFin)
        {
            string strTemporada = fechaInicio.ToString("yy") + "/" + fechaFin.ToString("yy");
            this.fechaInicio = fechaInicio;
            this.fechaFin = fechaFin;
            this.partidos = new List<cPartido>();
            this.temporada = strTemporada;
        }


    }


   



}

