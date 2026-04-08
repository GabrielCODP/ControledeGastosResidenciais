using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenseControl.Communication.Response.Financial.FinancialSummary
{
    public class ResponsePeopleFinancialSummaryJson
    {
        public IList<ResponsePersonFinancialSummaryJson> People { get; set; } = [];

        public ResponseFinancialTotalsJson Totals { get; set; } = new();
    }
}
