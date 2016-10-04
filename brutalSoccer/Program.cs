using Accord.Controls;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.Statistics.Kernels;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace brutalSoccer
{
   
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        static void Main(string[] args)
        {
            AllocConsole();
            EjecucionPostCreacion();
            // Create a simple binary AND
            // classification problem:

            double[][] problem =
            {
    //             a    b    a + b
    new double[] { 0,   0,     0    },
    new double[] { 0,   1,     0    },
    new double[] { 1,   0,     0    },
    new double[] { 1,   1,     1    },
};

            // Get the two first columns as the problem
            // inputs and the last column as the output

            // input columns
            double[][] inputs = problem.GetColumns(0, 1);

            // output column
            int[] outputs = problem.GetColumn(2).ToInt32();

            // Plot the problem on screen
            ScatterplotBox.Show("AND", inputs, outputs).Hold();

        }

        private static void EjecucionPostCreacion()
        {
            cModelo m = new cModelo();
            cManager manager = m.Managers.FirstOrDefault();
            manager.ProcesaClasificacion();
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
