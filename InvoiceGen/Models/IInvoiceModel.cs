using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen.Models
{
    public interface IInvoiceModel
    {
        string[] ValidMonths { get; }
        bool IsValidMonth(string month);
        bool IsMonthlyInvoice(string title);

        string GetAmountToDisplay(decimal amount);
        string GetAmountToDisplayAsTotal(decimal amount);
        bool AmountEntryValid(string amount);
        decimal GetTotalAmountFromList(IEnumerable<Tuple<InvoiceItem, int>> listEntries);
    }
}
