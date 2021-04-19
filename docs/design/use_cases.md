# Use cases

## UC-1: Create new invoice
1. "View or Generate" tab in main window is active. The main window is in the "ready" state - only the "New Invoice" button in the current tab is enabled. User clicks "New Invoice" button. "Monthly" radio button in "Title" group is selected, month combo box and year text box are activated. "New Invoice" button is disabled.

2. User selects month from combo box, and enters a valid year. The "Description" and "Amount" text boxes and the "Quantity" number picker in the "New Item" group are activated.

### Variation: In step 2, the user selects "Custom" invoice type:
2.1. User enters description in the text box. The "Description" and "Amount" text boxes and the "Quantity" number picker in the "New Item" group are activated.

Proceed to Step 3.

3. User adds a description and valid amount, and selects the quantity. The "Add" button in the "New Item" group is activated, and the items list below is also activated.

4. User clicks the "Add" button in the "New Item" group. The newly-added item appears in the items list below.

5. The user may duplicate an item by selecting it in the list, and clicking the "Duplicate Selected" button, at which point the "Quantity" of the item in the list is doubled. The user may delete an item from the list by selecting it, then clicking the "Remove Selected" button. If no item is selected, the "Duplicate Selected" and "Remove Selected" buttons are disabled.

6. User clicks "Save and Export XLSX" button. 
* All controls within the "View or Generate" tab are disabled.
* Invoice is saved to records. Status bar colour is changed to "in progress", status bar text is changed to "Saving to Records In Progress".
* Folder Browser Dialog appears. User selects directory. User clicks "OK" button. 
* Spreadsheet saved to file system. status bar colour changed "in progress", status bar text changed to "Exporting Spreadsheet In Progress".
* If exporting spreadsheet successful, status bar colour changed to "completed", status bar text changed to "Exporting Spreadsheet Completed Successfully".

7. UI returned to "ready" state. END.

### Exception: failed to save to records:
6.1. Status bar colour changed to "error". Status bar text changed to "Error Saving to Records". UI remains in current state, user can make another attempt. END.

### Exception: failed to save spreadsheet:
6.1. Status bar colour changed to "error". Status bar text changed to "Error Exporting Spreadsheet". UI returned to "ready" state. END.

### Variation: user clicks "Save and Email" button:
6.1. User clicks "Save and Email" button.

* All controls within the "View or Generate" tab are disabled.
* Check if invoice with given title already exists. If exists, show a dialog asking the user to enter a different title. If not exists, proceed.
* Status bar colour set to "in progress" colour. Status bar text set to "Sending Email In Progress".
* Email is sent.

### Exception: sending to email failed:

### Variation: user cancels creating new invoic:
1.1. At any point, the user may abort this procedure by clicking the "Cancel" button. The window returns to the "ready" state.

## UC-2: Update invoice Paid status
1. "History" tab in the main window is active. The main window is in the "ready" state - only the "New Invoice" button in the current tab is enabled. User clicks "New Invoice" button. "Monthly" radio button in "Title" group is selected, month combo box and year text box are activated. "New Invoice" button is disabled. `invoices.xml` contains at least one invoice record. "Update Records" button is disabled.

2. User selects an invoice record in the history. User clicks the "Paid" checkbox for that record. Checkbox state is updated. "Update Records" button is enabled.

3. User clicks "Update Records" button. Status bar displays "Updating Records In Progress" and the in-progress colour.

4. Records in `invoices.xml` updated. Records are reloaded in the history grid in the "History" tab. Status bar colour changed to completed colour. Status bar text changed to "Updating Records Completed Successfully".

### Exception: failed to save to records:
4.1. Status bar changed to error colour. Status bar text changed to "Error Saving To Records". UI remains in current state, user can make another attempt. END.

## UC-3: View invoice from invoice history
1. "History" tab in the main window is active. The main window is in the "ready" state - only the "New Invoice" button in the current tab is enabled. User clicks "New Invoice" button. "Monthly" radio button in "Title" group is selected, month combo box and year text box are activated. "New Invoice" button is disabled. `invoices.xml` contains at least one invoice record. "Update Records" button is disabled.

2. User selects an invoice record in the history. User clicks "View Selected" button. The "View or Generate" tab in the main window becomes active.
- If the invoice is a monthly type, the "Monthly" check box is checked, and the month combo box and year text box are populated with the appropriate values.
- Else if the invoice is a custom type, the "Custom" check box is checked, and the custom title text box is populated with the description.

The items data grid is populated with the invoice items and their details.
The texts of the save buttons on the lower LHS are changed to "Email" and "Export XLSX", and they are both enabled.
The "Cancel" button is enabled. END.

## UC-4: Export invoice from invoice history to spreadsheet
1. User carries out UC-3.

2. User clicks "Export XLSX" button. Standard folder browser dialog appears.

3. User selects directory in folder browser dialog, or creates a new one via the "Make New Folder" button, and selects it. User clicks "OK" button.

4. Spreadsheet exported to disk at chosen location.
    * Status bar colour changed to in progress colour.
    * Status bar message set to "Exporting Spreadsheet In Progress".

5. 
    * Status bar colour changed to completed successfully colour.
    * Status bar message set to "Exporting Spreadsheet Completed Successfully."

## UC-5: Send invoice from invoice history via email
1. User carries out UC-3.

2. User clicks "Email" button. 

## UC-6: Configure email
1. User clicks "Settings" menu. Settings menu appears, with a single menu item - "Email Configuration".

2. User clicks "Email Configuration" menu item. "Configuration" dialog box appears.

3. User edits information.

4. User clicks "Save" button. Information is saved to configuration, and the application closes.

## Variation: User clicks "Cancel" button:
4.1. User clicks "Cancel" button. The window closes, and configuration is unchanged.
