using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using InvoiceGen.Models.ObjectModel;

namespace InvoiceGen.Models
{
    /// <summary>
    /// Contains logic for creating and displaying invoices.
    /// </summary>
    public class InvoiceModel : IInvoiceModel
    {
        public static readonly string AMOUNT_PATTERN = @"^\d+\.(\d{2})$";

        public string[] ValidMonths
        {
            get => new string[] { "January", "February", "March", "April", "May", "June",
                                    "July", "August", "September", "October", "November", "December" };
        }

        /// <summary>
        /// Check if a month is a valid entry.
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool IsValidMonth(string month)
        {
            return ValidMonths.Contains(month);
        }

        /// <summary>
        /// Check if a title follows the pattern of a monthly invoice.
        /// Must be in the form "{month} {year}".
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool IsMonthlyInvoice(string title)
        {
            foreach (var m in ValidMonths)
            {
                string pattern = "^" + m + @" \d+$";
                if (Regex.IsMatch(title, pattern))
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
            return false;
        }

        /// <summary>
        /// Get a currency-formatted amount.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string GetAmountToDisplay(decimal amount)
        {
            return amount.ToString("C2", new System.Globalization.CultureInfo("en-AU"));
        }

        /// <summary>
        /// Get a currency-formatted amount with a "Total: " label.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string GetAmountToDisplayAsTotal(decimal amount)
        {
            return "Total: " + this.GetAmountToDisplay(amount);
        }
        
        /// <summary>
        /// Validates an item amount entry.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool AmountEntryValid(string amount)
        {
            if (Regex.IsMatch(amount, AMOUNT_PATTERN))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get the total cost of all the items displayed in the list.
        /// </summary>
        /// <param name="listEntries"></param>
        /// <returns></returns>
        public decimal GetTotalAmountFromList(IEnumerable<Tuple<InvoiceItem, int>> listEntries)
        {
            decimal total = 0;
            foreach (var entry in listEntries)
            {
                total += entry.Item1.Amount * entry.Item2;
            }

            return total;
        }//GetTotalAmountFromList
    }//class
}