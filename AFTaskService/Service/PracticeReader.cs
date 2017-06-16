using AFTaskModel.Model;
using AFTaskService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTaskService.Service
{
    public class PracticeReader: IPracticeReader
    {
        private string workingText;
        public Practices ExtractPractices(string lineAsCsv)
        {
            workingText = lineAsCsv;
            Practices practice = new Practices();

            //parseDataAsString();

            practice.Period = parseDataAsString().Trim();
            practice.ReferencePrac = parseDataAsString().Trim();
            practice.PracticeName = parseDataAsString().Trim();

            //parseDataAsString();

            practice.HealthCentre = parseDataAsString().Trim();
            practice.Address = parseDataAsString().Trim();
            practice.City = parseDataAsString().Trim();
            practice.County = parseDataAsString().Trim();
            practice.PostCode = parseDataAsString().Trim();

            return practice;
        }

        private String parseDataAsString()
        {
            string nextValue = null;
            //field, might, or might not, start with double-quotes
            if (workingText.StartsWith("\""))
            {
                int endOfValue = workingText.IndexOf("\"", 1);
                nextValue = workingText.Substring(1, endOfValue - 1);
                workingText = workingText.Substring(endOfValue + 2);
            }
            else
            {
                int endOfValue = workingText.IndexOf(",", 0);

                if (endOfValue != -1)
                {
                    nextValue = workingText.Substring(0, endOfValue);
                    workingText = workingText.Substring(endOfValue + 1);
                }
                else nextValue = workingText;

            }
            return nextValue;
        }
    }
}
