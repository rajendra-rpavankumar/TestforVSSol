using AFTaskModel.Model;
using AFTaskService.Interface;
using AFTaskService.Service;
using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTask.Mapper
{
    class CombinedDataMapper:MapperBase
    {
        private IPracticeReader practiceReader;
        private IPrescriptionReader prescriptionReader;

        public override void Initialize(MapperContext context)
        {
            practiceReader = new PracticeReader();
            prescriptionReader = new PrescriptionReader();
            base.Initialize(context);
        }

        public override void Map(string inputLine, MapperContext context)
        {
            char[] delimiterChars = { ',' };

            //split up the passed in line
            string[] individualItems = inputLine.Trim().Split(delimiterChars);

            //Step to Identify the Practice or Prescription data set
            //Using the SDK, tried to use the MapperContext.InputFileName property: it is always empty
            //So decided to use items count of each datasets
            //Items count 8 for practice and Items count 9 for prescription

            if (individualItems.Count() == 2)
            {
                Practices practice = practiceReader.ExtractPractices(inputLine);

                if (String.IsNullOrWhiteSpace(practice.ReferencePrac)) { return; }  //Ignore, practise name cannot be null
                if (String.IsNullOrWhiteSpace(practice.PracticeName)) { return; }  //Ignore, practise name cannot be null
                if (String.IsNullOrWhiteSpace(practice.PostCode)) { return; }  //Ignore, practise name cannot be null

                context.EmitKeyValue(practice.ReferencePrac, Convert.ToString(practice.PostCode));
            }

            //Items count 9 for prescription

            if (individualItems.Count() == 2)
            {
                Prescriptions prescription = prescriptionReader.ExtractPrescriptions(inputLine);

                if (String.IsNullOrWhiteSpace(prescription.PracticeId)) { return; }  //Ignore, practise name cannot be null

                // parctice Id with Amount

                context.EmitKeyValue(prescription.PracticeId, prescription.ActualCost.ToString("0.00"));
            }
        }
    }
}
