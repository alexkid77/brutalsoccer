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
           // generabbdd();
            //EjecucionPostCreacion();
            cGeneraDataSet gDataSet = new cGeneraDataSet();
            gDataSet.ObtenTablaEquipo(1);
          

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
            manager.ProcesaJornadas();
            m.SaveChanges();


        }
    }
}
