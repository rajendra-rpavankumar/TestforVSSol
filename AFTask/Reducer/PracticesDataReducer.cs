using Microsoft.Hadoop.MapReduce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTask.Reducer
{
    public class PracticesDataReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            string outputText = "Total Number of Practises in LONDON - " + values.Count();
            context.EmitKeyValue(key, outputText);
        }
    }
}
