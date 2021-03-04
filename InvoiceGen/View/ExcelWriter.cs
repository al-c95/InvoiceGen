using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;
using InvoiceGen.Model.ObjectModel;

namespace InvoiceGen.View
{
    /// <summary>
    /// Exports new invoices as XLSX spreadsheets.
    /// </summary>
    public class ExcelWriter
    {
        // Excel package and worksheet
        private string _fileName;
        private ExcelPackage _pck;
        private ExcelWorksheet _ws; // just using one worksheet

        // styles and formatting
        private const int HEADER_ROW = 5;
        private const string META_AND_HEADER_STYLE = "Metadata and Header";
        private const string TOTAL_STYLE_WITH_CURRENCY = "Metadata and header with currency formatting";
        private const string EVEN_ROW_STYLE = "Even";
        private const string ODD_ROW_STYLE = "Odd";
        private const string EVEN_ROW_STYLE_WITH_CURRENCY = "Even with currency formatting";
        private const string ODD_ROW_STYLE_WITH_CURRENCY = "Odd with currency formatting";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="directory">Where to save the spreadsheet.</param>
        /// <param name="title">Invoice title.</param>
        /// <param name="from">Sender.</param>
        /// <param name="to">Recipient.</param>
        public ExcelWriter(string directory, string title, string from, string to)
        {
            // create the package and worksheet
            this._pck = new ExcelPackage();
            this._ws = this._pck.Workbook.Worksheets.Add(title);

            // create and add the named styles

            OfficeOpenXml.Style.XmlAccess.ExcelNamedStyleXml namedStyle;
            Color colour;

            namedStyle = _pck.Workbook.Styles.CreateNamedStyle(META_AND_HEADER_STYLE);
            namedStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
            colour = Color.FromArgb(255, 0, 0, 170);
            namedStyle.Style.Fill.BackgroundColor.SetColor(colour);
            namedStyle.Style.Font.Bold = true;
            colour = Color.FromArgb(255, 255, 255, 255);
            namedStyle.Style.Font.Color.SetColor(colour);
            namedStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            namedStyle.Style.Border.Left.Style = ExcelBorderStyle.Thick;
            namedStyle.Style.Border.Right.Style = ExcelBorderStyle.Thick;
            namedStyle.Style.Border.Left.Color.SetColor(colour);
            namedStyle.Style.Border.Right.Color.SetColor(colour);

            namedStyle = _pck.Workbook.Styles.CreateNamedStyle(TOTAL_STYLE_WITH_CURRENCY);
            namedStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
            colour = Color.FromArgb(255, 0, 0, 170);
            namedStyle.Style.Fill.BackgroundColor.SetColor(colour);
            namedStyle.Style.Font.Bold = true;
            colour = Color.FromArgb(255, 255, 255, 255);
            namedStyle.Style.Font.Color.SetColor(colour);
            namedStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            namedStyle.Style.Border.Left.Style = ExcelBorderStyle.Thick;
            namedStyle.Style.Border.Right.Style = ExcelBorderStyle.Thick;
            namedStyle.Style.Border.Left.Color.SetColor(colour);
            namedStyle.Style.Border.Right.Color.SetColor(colour);
            namedStyle.Style.Numberformat.Format = "$#,##0.00";

            namedStyle = _pck.Workbook.Styles.CreateNamedStyle(EVEN_ROW_STYLE);
            namedStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
            colour = Color.FromArgb(255, 239, 239, 255);
            namedStyle.Style.Fill.BackgroundColor.SetColor(colour);

            namedStyle = _pck.Workbook.Styles.CreateNamedStyle(ODD_ROW_STYLE);
            namedStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
            colour = Color.FromArgb(255, 207, 207, 255);
            namedStyle.Style.Fill.BackgroundColor.SetColor(colour);

            namedStyle = _pck.Workbook.Styles.CreateNamedStyle(EVEN_ROW_STYLE_WITH_CURRENCY);
            namedStyle.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            colour = Color.FromArgb(255, 239, 239, 255);
            namedStyle.Style.Fill.BackgroundColor.SetColor(colour);
            namedStyle.Style.Numberformat.Format = "$#,##0.00";

            namedStyle = _pck.Workbook.Styles.CreateNamedStyle(ODD_ROW_STYLE_WITH_CURRENCY);
            namedStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
            colour = Color.FromArgb(255, 207, 207, 255);
            namedStyle.Style.Fill.BackgroundColor.SetColor(colour);
            namedStyle.Style.Numberformat.Format = "$#,##0.00";

            // generate the filename from title and directory
            this._fileName = directory + "\\Invoice " + title + ".xlsx"; 

            // write and style headers and metadata
            this._ws.Cells[1, 1].Value = "Invoice: ";
            this._ws.Cells[1, 1].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[1, 2].Value = title;
            this._ws.Cells[1, 2].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[2, 1].Value = "From:";
            this._ws.Cells[2, 1].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[2, 2].Value = from;
            this._ws.Cells[2, 2].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[3, 1].Value = "To:";
            this._ws.Cells[3, 1].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[3, 2].Value = to;
            this._ws.Cells[3, 2].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[HEADER_ROW, 1].Value = "Description";
            this._ws.Cells[HEADER_ROW, 1].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[HEADER_ROW, 2].Value = "Amount";
            this._ws.Cells[HEADER_ROW, 2].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[HEADER_ROW, 3].Value = "Quantity";
            this._ws.Cells[HEADER_ROW, 3].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[HEADER_ROW, 4].Value = "Item Total";
            this._ws.Cells[HEADER_ROW, 4].StyleName = META_AND_HEADER_STYLE;
        }

        /// <summary>
        /// Add rows for each of the items and style them.
        /// </summary>
        /// <param name="items">A list of tuples of invoice items and their quantities.</param>
        public void AddItems(List<Tuple<InvoiceItem, int>> items)
        {
            // write and style each item record 
            int row = HEADER_ROW + 1;
            foreach (var i in items)
            {
                this._ws.Cells[row, 1].Value = i.Item1.Description;

                this._ws.Cells[row, 2].Value = Convert.ToDecimal(i.Item1.Amount);

                this._ws.Cells[row, 3].Value = i.Item2;

                this._ws.Cells[row, 4].Formula = string.Format("{0}*{1}", this._ws.Cells[row, 2].Address, this._ws.Cells[row, 3].Address);

                if (row%2 == 0)
                {
                    this._ws.Cells[row, 1].StyleName = EVEN_ROW_STYLE;
                    this._ws.Cells[row, 2].StyleName = EVEN_ROW_STYLE_WITH_CURRENCY;
                    this._ws.Cells[row, 3].StyleName = EVEN_ROW_STYLE;
                    this._ws.Cells[row, 4].StyleName = EVEN_ROW_STYLE_WITH_CURRENCY;
                }
                else
                {
                    this._ws.Cells[row, 1].StyleName = ODD_ROW_STYLE;
                    this._ws.Cells[row, 2].StyleName = ODD_ROW_STYLE_WITH_CURRENCY;
                    this._ws.Cells[row, 3].StyleName = ODD_ROW_STYLE;
                    this._ws.Cells[row, 4].StyleName = ODD_ROW_STYLE_WITH_CURRENCY;
                }

                row++;
            }

            // write and style the total
            string finalItemTotalAddress = this._ws.Cells[row-1, 4].Address;
            string totalLabelAddress = this._ws.Cells[row, 3].Address;
            string totalValueAddress = this._ws.Cells[row, 4].Address;
            this._ws.Cells[totalLabelAddress].Value = "TOTAL: ";
            this._ws.Cells[totalLabelAddress].StyleName = META_AND_HEADER_STYLE;
            this._ws.Cells[totalValueAddress].Formula = string.Format("=SUM({0}:{1})", "D6", finalItemTotalAddress);
            this._ws.Cells[totalValueAddress].StyleName = TOTAL_STYLE_WITH_CURRENCY;

            // calculate values from formulas
            //this._ws.Calculate();

            // clean up 
            // autofit 
            this._ws.Cells.AutoFitColumns();
        }

        /// <summary>
        /// Save the file and dispose of the package.
        /// </summary>
        public void CloseAndSave()
        {
            FileInfo fi = new FileInfo(this._fileName);
            _pck.SaveAs(fi);
            _pck.Dispose();
        }

        /// <summary>
        /// Get a memory stream of the spreadsheet and dispose of the package.
        /// </summary>
        /// <returns></returns>
        public MemoryStream CloseAndGetMemoryStream()  
        {
            MemoryStream ms = new MemoryStream(_pck.GetAsByteArray());
            _pck.Dispose();

            return ms;
        }
    }
}
