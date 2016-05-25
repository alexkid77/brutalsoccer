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
            cManager manager = new cManager();
            List<cLinea> lineas = new List<cLinea>();
            string[] files = System.IO.Directory.GetFiles(".", "*.csv");
            foreach (string file in files)
            {
                TextReader textReader = File.OpenText(file);
                CsvReader csv = new CsvReader(textReader);

                while (csv.Read())
                {
                    lineas.Add(new cLinea(csv));

                }

                List<string> gfdgfd = lineas.Select(p => p.visitante).Distinct().ToList();

                manager.procesaTemporada(lineas);
            }
          

            
            lineas = lineas.OrderBy(p => p.fecha).ToList(); ;

        }
    }
}
