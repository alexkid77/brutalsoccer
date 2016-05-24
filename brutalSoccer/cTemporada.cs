using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cTemporada
    {
        DateTime fechaInicio { get; set; }
        DateTime fechaFin { get; set; }

    }

    public class cEquipo
    {

    }

    public class cPartido
    {
        public cEquipo Local { get; set; }
        public cEquipo Visitante { get; set; }

    }


}
