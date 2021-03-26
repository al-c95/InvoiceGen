# Concept Design

## Introduction
InvoiceGen is a simple windows application designed to produce monthly or "custom" (with a textual description) invoices as Microsoft Excel spreadsheets, based on items the user enters on the screen. It displays a running total as they are entered. The spreadsheets can be saved to disk or emailed.

A history of issued invoices is maintained, and they can be reviewed by selecting them from a list, and spreadsheets re-generated to be saved to disk or emailed.

There is an email configuration window for specifying the sender's and recipient's details.

This and other design-related documents will be updated as requirements change.

## Requirements
### R-1
The application should allow the user to generate invoices by manually entering items, while the running total is displayed.

### R-2
The application should allow invoices to be issued as spreadsheets, which can be sent via email or saved to disk.

### R-3
The application should maintain a history of invoices.

### R-4
The application should allow invoices maintained in the history to be re-issued as spreadsheets.

### R-5
The application should allow the user to update the status of each invoice.

### R-6
The application should allow the user to configure email settings for the sender and recipient.

## Architecture
The application is built on .NET WinForms. It uses a Model-View-Presenter architecture, for enhanced unit-testability and separation of concerns.

Sending emails is facilitated by the use of an SMTP client within the software.

The invoice records are stored in an XML file in the application directory. Whilst XML is usually more of a data transmission medium than a data storage medium, and usually an Sqlite database or similar would be used for this sort of application, the idea was to have text-editable records.