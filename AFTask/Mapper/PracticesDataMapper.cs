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

    public class PracticesDataMapper : MapperBase
    {

        private IPracticeReader reader;

        public override void Initialize(MapperContext context)
        {
            reader = new PracticeReader();
            base.Initialize(context);
        }
        public override void Map(string inputLine, MapperContext context)
        {
            char[] delimiterChars = { ',' };

            //split up the passed in line
            string[] individualItems = inputLine.Trim().Split(delimiterChars);

            if (individualItems.Count() == 8)
            {
                Practices practice = reader.ExtractPractices(inputLine);

                if (String.IsNullOrWhiteSpace(practice.ReferencePrac)) { return; }  //Ignore, practise name cannot be null
                if (String.IsNullOrWhiteSpace(practice.PracticeName)) { return; }  //Ignore, practise name cannot be null
                if (String.IsNullOrWhiteSpace(practice.County)) { return; }  //Ignore, practise name cannot be null

                context.EmitKeyValue("LONDON", Convert.ToString(practice.County == "LONDON"));
            }
        }
    }
}
