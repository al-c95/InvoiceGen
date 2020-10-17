# InvoiceGen

## Description and Requirements

An application that generates simple Excel spreadsheet based invoices. It was designed to solve a specific problem where monthly invoices were issued. Invoices are issued as spreadsheets and can be saved to disk and/or emailed.

It is based on WinForms and targets .NET Framework version 4.6.1. This is the only dependency required in order to run it.

Invoice records are stored in an XML file, `invoices.xml`, located in the same directory as the executable. 

**Current version: v1.0.0 (beta)**

## Contents
This repository is based around a Microsoft Visual Studio solution. The directories `InvoiceGen` and `InvoiceGen_Tests` contain the source code, that is, the production code and unit tests, respectively. The `docs` directory contains both design and usage documentation.

## Installation and Setup
No installation process is required. Simply place the `\InvoiceGen\bin\Release` folder somewhere on disk, and execute `InvoiceGen.exe` to run the application. The `invoices.xml` file mentioned above contains some example invoice records.

## Architecture
The application is based around a simple Model-View-Presenter (MVP) pattern for enhanced testability. The code is constantly maintained to make sure it is as compliant as possible with this architectural pattern.

## Usage
Some instructions or a manual will provided as part of a future release.

## Attributions

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)

