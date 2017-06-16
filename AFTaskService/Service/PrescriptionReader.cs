using AFTaskModel.Model;
using AFTaskService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTaskService.Service
{
    public class PrescriptionReader:IPrescriptionReader
    {
        private string workingText;

        public Prescriptions ExtractPrescriptions(string lineAsCsv)
        {
            workingText = lineAsCsv;
            Prescriptions prescription = new Prescriptions();

            prescription.SHA = parseDataAsString().Trim();
            prescription.PCT = parseDataAsString().Trim();
            prescription.PracticeId = parseDataAsString().Trim();
            prescription.BNFCode = parseDataAsString().Trim();
            prescription.BNFName = parseDataAsString().Trim();
            prescription.Items = parseDataAsInt();
            prescription.NIC = parseDataAsDecimal();
            prescription.ActualCost = parseDataAsDecimal();
            prescription.Period = parseDataAsString();


            return prescription;
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

        private int parseDataAsInt()
        {
            String intAsString = parseDataAsString();
            intAsString = intAsString.Replace("$", "");
            intAsString = intAsString.Replace(",", "");
            intAsString = intAsString.Replace(" ", "");

            int value;
            if (int.TryParse(intAsString, out value))
            {
                // It's a decimal
                return value;
            }
            else
            {
                // No it's not.
                return 0;
            }
        }

        private decimal parseDataAsDecimal()
        {
            String deciamlAsString = parseDataAsString();
            deciamlAsString = deciamlAsString.Replace("$", "");
            deciamlAsString = deciamlAsString.Replace(",", "");
            deciamlAsString = deciamlAsString.Replace(" ", "");

            decimal value;
            if (Decimal.TryParse(deciamlAsString, out value))
            {
                // It's a decimal
                return value;
            }
            else
            {
                // No it's not.
                return 0;
            }

        }
    }
}
