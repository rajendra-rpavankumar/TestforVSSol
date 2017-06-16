using AFTask.Mapper;
using AFTask.Reducer;
using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load Data in to HDInsight 
            HadoopJobConfiguration practice_jobConfig = new HadoopJobConfiguration()
            {
                InputPath = "Input/AFTask",
                OutputFolder = "Output/AFTaskPractices",
            };
            HadoopJobConfiguration prescription_jobConfig = new HadoopJobConfiguration()
            {
                InputPath = "Input/AFTask",
                OutputFolder = "Output/AFTaskPrescriptions",
            };

            HadoopJobConfiguration combined_jobConfig = new HadoopJobConfiguration()
            {
                InputPath = "Input/AFTask",
                OutputFolder = "Output/AFTaskCombined",
            };

            // Call Jobs 

            // Question 1 How many practices are in London?
            Hadoop.Connect().MapReduceJob.Execute<PracticesDataMapper, PracticesDataReducer>(practice_jobConfig);

            // Question 2 What was the average actual cost of all peppermint oil prescriptions?
            Hadoop.Connect().MapReduceJob.Execute<PrescriptionsDataMapper, PrescriptionsDataReducer>(prescription_jobConfig);

            // Question 3 Which 5 post codes have the highest actual spend, and how much did each spend in total?
            Hadoop.Connect().MapReduceJob.Execute<CombinedDataMapper, CombinedDataReducer>(combined_jobConfig);

            System.Console.Read();  //using to catch console
        }
    }
}
