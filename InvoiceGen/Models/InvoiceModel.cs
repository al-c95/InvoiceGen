﻿//MIT License

//Copyright (c) 2020-2024

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

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