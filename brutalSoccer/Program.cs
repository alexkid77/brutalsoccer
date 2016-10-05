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
using Accord.MachineLearning.Bayes;
using System.Data;
using Accord.Statistics.Filters;
using Accord;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Distributions.Univariate;
using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;

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

            //  generabbdd();
            //EjecucionPostCreacion();
            cModelo m = new cModelo();
            List<int> lid = new List<int>();
            int x = 0;
            foreach (var i in m.Equipos)
            {
                lid.Add(i.Id);
                if (x == 20)
                    break;
                x++;
            }
            lid.RemoveAt(0);
            cGeneraDataSet gDataSet = new cGeneraDataSet();
            DataTable t = gDataSet.ObtenTablaEquipo(lid.ToArray());
            DataTable t2 = gDataSet.ObtenTablaEquipo(1);
            Codification codebook = new Codification(t,
      "posicion", "numeroPartidosJugados", "numeroPartidosGanados", "numeroPartidosPerdidos", "numeroPartidosEmpadados", "ResultadoPartido", "golesAcumuladosAfavorVisitante", "golesAcumuladosAfavorLocal", "golesAcumuladosEnContraVisitante", "golesAcumuladosEnContraLocal", "numeroGolesAfavorLocal", "numeroGolesAfavorVisitante", "numeroGolesEnContraLocal", "numeroGolesEnContraVisitante", "ptosStandar", "diferenciaGoles");
            DataTable symbols = codebook.Apply(t);
            /*original*/
             Double[][] inputs = symbols.ToArray<double>("IdEquipo","IdEquipoAdversario", "numeroPartidosGanados", "numeroPartidosPerdidos", "numeroPartidosEmpadados",  "golesAcumuladosAfavorVisitante", "golesAcumuladosAfavorLocal", "golesAcumuladosEnContraVisitante", "golesAcumuladosEnContraLocal", "ptosStandar", "diferenciaGoles");
         //   double[][] inputs = symbols.ToArray<double>("posicion", "numeroPartidosGanados", "numeroPartidosPerdidos", "numeroPartidosEmpadados", "golesAcumuladosAfavorVisitante", "golesAcumuladosAfavorLocal", "golesAcumuladosEnContraVisitante", "golesAcumuladosEnContraLocal",  "ptosStandar");
            int[] outputs = symbols.ToArray<int>("ResultadoPartido");
            /*original*/
            double[][] entrada2=   codebook.Apply(t).ToArray<double>("IdEquipo", "IdEquipoAdversario", "numeroPartidosGanados", "numeroPartidosPerdidos", "numeroPartidosEmpadados", "golesAcumuladosAfavorVisitante", "golesAcumuladosAfavorLocal", "golesAcumuladosEnContraVisitante", "golesAcumuladosEnContraLocal", "ptosStandar", "diferenciaGoles");


            /*
            // Now, let's create the C4.5 algorithm
            ID3Learning id3 = new ID3Learning();

            // and learn a decision tree. The value of
            //   the error variable below should be 0.
            // 
            DecisionTree tree = id3.Learn(inputs, outputs);
          double errorarbol=  id3.ComputeError(inputs, outputs);
            for (int kk = 0; kk < entrada2.Length; kk++)
            {
                int val = tree.Decide(entrada2[kk]);
                string result = codebook.Translate("ResultadoPartido", val);
            }*/

            var teacher = new MulticlassSupportVectorLearning<Gaussian>()
            {
                // Configure the learning algorithm to use SMO to train the
                //  underlying SVMs in each of the binary class subproblems.
                Learner = (param) => new SequentialMinimalOptimization<Gaussian>()
                {
                    // Estimate a suitable guess for the Gaussian kernel's parameters.
                    // This estimate can serve as a starting point for a grid search.
                    UseKernelEstimation = true
                }
               
            };

            var machine = teacher.Learn(inputs, outputs);



            // Create the multi-class learning algorithm for the machine
            var calibration = new MulticlassSupportVectorLearning<Gaussian>()
            {
                Model = machine, // We will start with an existing machine

                // Configure the learning algorithm to use SMO to train the
                //  underlying SVMs in each of the binary class subproblems.
                Learner = (param) => new ProbabilisticOutputCalibration<Gaussian>()
                {
                    Model = param.Model // Start with an existing machine
                }
            };


            // Configure parallel execution options
            calibration.ParallelOptions.MaxDegreeOfParallelism = 1;

            // Learn a machine
            calibration.Learn(inputs, outputs);

            // Obtain class predictions for each sample
            int[] predicted = machine.Decide(inputs);

            // Get class scores for each sample
            double[] scores = machine.Score(inputs);

            // Get log-likelihoods (should be same as scores)
            double[][] logl = machine.LogLikelihoods(inputs);

            // Get probability for each sample
            double[][] prob = machine.Probabilities(inputs);

            // Compute classification error
            double error = new ZeroOneLoss(outputs).Loss(predicted);
            double loss = new CategoryCrossEntropyLoss(outputs).Loss(prob);
            for (int kk = 0; kk < entrada2.Length; kk++)
            { 
            
                int val = machine.Decide(entrada2[kk]);
                string result = codebook.Translate("ResultadoPartido", val);
                double pro = machine.Probability(entrada2[kk], val);
            }
           

    
        
            /*var learner = new NaiveBayesLearning<NormalDistribution>();

            try
            {

                var nb = learner.Learn(inputs, outputs);
                double[] salida;
                string ant="";
                for (int kk = 0; kk < entrada2.Length; kk++)
                {
                    int jjjj = 2;
                    var val = entrada2[kk];
                    int x = nb.Decide(val);
                    string result = codebook.Translate("ResultadoPartido", x);
                    if (result != ant)
                    {
                        jjjj++;
                        ant = result;
                    }
                }
            }
            catch (Exception ex)
            {
                int x = 0;
                x++;
            }*/


            /*     
                   // Let's say we have the following data to be classified
                   // into three possible classes. Those are the samples:
                   // 
                   double[][] inputs =
                   {
           //               input         output
           new double[] { 0, 1, 1, 0 }, //  0 
           new double[] { 0, 1, 0, 0 }, //  0
           new double[] { 0, 0, 1, 0 }, //  0
           new double[] { 0, 1, 1, 0 }, //  0
           new double[] { 0, 1, 0, 0 }, //  0




       };

                   int[] outputs = // those are the class labels
                   {
           0, 0, 0, 0, 1,


       };

                   // Create a one-vs-one multi-class SVM learning algorithm 
                   var teacher = new MulticlassSupportVectorLearning<Gaussian>()
                   {
                       // Configure the learning algorithm to use SMO to train the
                       //  underlying SVMs in each of the binary class subproblems.
                       Learner = (param) => new SequentialMinimalOptimization<Gaussian>()
                       {
                           // Estimate a suitable guess for the Gaussian kernel's parameters.
                           // This estimate can serve as a starting point for a grid search.
                           UseKernelEstimation = true,
                           UseComplexityHeuristic = true

                       }
                   };

                   // Configure parallel execution options
                   teacher.ParallelOptions.MaxDegreeOfParallelism = 1;

                   // Learn a machine
                   var machine = teacher.Learn(inputs, outputs);



                   // Create the multi-class learning algorithm for the machine
                   var calibration = new MulticlassSupportVectorLearning<Gaussian>()
                   {
                       Model = machine, // We will start with an existing machine

                       // Configure the learning algorithm to use SMO to train the
                       //  underlying SVMs in each of the binary class subproblems.
                       Learner = (param) => new ProbabilisticOutputCalibration<Gaussian>()
                       {
                           Model = param.Model // Start with an existing machine
                       }
                   };


                   // Configure parallel execution options
                   calibration.ParallelOptions.MaxDegreeOfParallelism = 1;

                   // Learn a machine
                   calibration.Learn(inputs, outputs);

                   // Obtain class predictions for each sample
                   int[] predicted = machine.Decide(inputs);

                   // Get class scores for each sample
                   double[] scores = machine.Score(inputs);

                   // Get log-likelihoods (should be same as scores)
                   double[][] logl = machine.LogLikelihoods(inputs);

                   // Get probability for each sample
                   double[][] prob = machine.Probabilities(inputs);

                   // Compute classification error
                   double error = new ZeroOneLoss(outputs).Loss(predicted);
                   double loss = new CategoryCrossEntropyLoss(outputs).Loss(prob);*/
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
        
            m.SaveChanges();
            manager.ProcesaJornadas();
            m.SaveChanges();

        }
    }
}
