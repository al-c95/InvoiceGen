//MIT License

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

using System.Xml.Linq;

namespace InvoiceGen.Models.DataAccessLayer
{
    /// <summary>
    /// Loads and saves the XML file.
    /// </summary>
    public class XmlFileHandler : IXmlFileHandler
    {
        private string _fileName; 
        private XDocument _doc;

        /// <summary>
        /// Constructor with filename dependency injection. Opens the file.
        /// </summary>
        /// <param name="fileName"></param>
        public XmlFileHandler(string fileName)
        {
            this._fileName = fileName;

            Load();
        }

        /// <summary>
        /// Save the XML file.
        /// </summary>
        public void SaveXMLFile(XDocument doc)
        {
            // first try saving it
            doc.Save(_fileName);

            // then reload it
            Load();
        }

        /// <summary>
        /// Return the XML as a string.
        /// </summary>
        /// <returns></returns>
        public string GetXML()
        {
            return _doc.ToString();
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        private void Load()
        {
            this._doc = XDocument.Load(_fileName);
        }//Load
    }//class
}
