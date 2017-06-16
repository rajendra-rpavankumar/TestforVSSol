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
    public class PrescriptionsDataMapper : MapperBase
    {
        private IPrescriptionReader reader;

        public override void Initialize(MapperContext context)
        {
            reader = new PrescriptionReader();
            base.Initialize(context);
        }
        public override void Map(string inputLine, MapperContext context)
        {
            char[] delimiterChars = { ',' };

            //split up the passed in line
            string[] individualItems = inputLine.Trim().Split(delimiterChars);

            if (individualItems.Count() == 9)
            {
                Prescriptions prescription = reader.ExtractPrescriptions(inputLine);

                if (String.IsNullOrWhiteSpace(prescription.PracticeId)) { return; }  //Ignore, practise name cannot be null

                //Filter by peppermint oil

                if (prescription.BNFName.ToLower() != "peppermint oil") { return; }  //Ignore, if filtor not matched

                context.EmitKeyValue(prescription.BNFName, prescription.ActualCost.ToString("0.00"));
            }
        }
    }
}
