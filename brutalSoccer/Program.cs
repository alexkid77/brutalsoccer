using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    class Program
    {

        static void Main(string[] args)
        {
            cModelo m = new cModelo();
         //  cTemporada e = m.Temporadas.FirstOrDefault();
           //    cManager manager = m.Managers.FirstOrDefault();
            generabbdd();
            // 
          //  manager.ProcesaClasificacion();
            //    manager.ProcesaClasificacion();
            //   cEntradasNeuro neuro = manager.equipos[0].temporadas[0].partidos[0].entradasNeuro;

            // lineas = lineas.OrderBy(p => p.fecha).ToList(); ;

        }

        private static void generabbdd()
        {
            cManager manager = new cManager();
            cModelo m = new cModelo();

            m.Managers.Add(manager);

            List<cLinea> lineas = new List<cLinea>();
            string[] files = System.IO.Directory.GetFiles(".", "*.csv");
            int x = 0;
            foreach (string file in files)
            {
                lineas.Clear();
                TextReader textReader = File.OpenText(file);
                CsvReader csv = new CsvReader(textReader);

                while (csv.Read())
                {
                    lineas.Add(new cLinea(csv));
                }

                List<string> gfdgfd = lineas.Select(p => p.visitante).Distinct().ToList();

                manager.procesaTemporada(lineas);


            }
            manager.ProcesaJornadas();
            m.SaveChanges();
          
        }
    }
}
