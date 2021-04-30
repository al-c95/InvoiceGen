﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        decimal GetTotalAmountFromList(IEnumerable<Tuple<InvoiceItem,int>> listEntries);
    }

    /// <summary>
    /// Contains logic for creating and displaying invoices.
    /// </summary>
    public class InvoiceModel : IInvoiceModel
    {
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
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool IsMonthlyInvoice(string title)
        {
            bool startsWithMonth = false;
            foreach (var m in ValidMonths)
            {
                if (title.StartsWith(m))
                    startsWithMonth = true;
            }

            if (startsWithMonth)
            {
                // check if it ends with a space followed by a year
                if (Regex.IsMatch(title, @"[A-Za-z]+ \d+"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
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
            if (Regex.IsMatch(amount, @"\d+\.(\d{2})"))
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
        }
    }
}
