using AFTaskModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTaskService.Interface
{
    public interface IPracticeReader
    {
        Practices ExtractPractices(string lineAsCsv);
    }
}
