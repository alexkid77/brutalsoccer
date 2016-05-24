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
        public void procesaLinea(cLinea linea)
        {
            this.addEquipo(linea.local);
            this.addEquipo(linea.visitante);
        }

        public void addEquipo(string nombre)
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
        DateTime fechaInicio { get; set; }
        DateTime fechaFin { get; set; }

    }

    public class cEquipo
    {
        public string Nombre { get; set; }
        List<cTemporada> partidos = new List<cTemporada>();
    }

    public class cPartido
    {
        public string Division { get; set; }
        public cEquipo Local { get; set; }
        public cEquipo Visitante { get; set; }
        
    }


}
