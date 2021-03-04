# Use cases

## Create new invoice
1. "View or Generate" tab in main window is active. The main window is in the "ready" state - only the "New Invoice" button in the current tab is enabled. User clicks "New Invoice" button. "Monthly" radio button in "Title" group is selected, month combo box and year text box are activated. "New Invoice" button is disabled.

2. User selects month from combo box, and enters a valid year. The "Description" and "Amount" text boxes and the "Quantity" number picker in the "New Item" group are activated.

### Variation: In step 2, the user selects "Custom" invoice type.
2.1. User enters description in the text box. The "Description" and "Amount" text boxes and the "Quantity" number picker in the "New Item" group are activated.

Proceed to Step 3.

3. User adds a description and valid amount, and selects the quantity. The "Add" button in the "New Item" group is activated, and the items list below is also activated.

4. User clicks the "Add" button in the "New Item" group. The newly-added item appears in the items list below.

5. The user may duplicate an item by selecting it in the list, and clicking the "Duplicate Selected" button, at which point the "Quantity" of the item in the list is doubled. The user may delete an item from the list by selecting it, then clicking the "Remove Selected" button. If no item is selected, the "Duplicate Selected" and "Remove Selected" buttons are disabled.

6. User clicks "Save and Export XLSX" button. 
- Invoice is saved to records. Status bar colour is changed to "in progress", status bar text is changed to "Saving to Records In Progress".
- Folder Browser Dialog appears. User selects directory. User clicks "OK" button. 
- Spreadsheet saved to file system. status bar colour changed "in progress", status bar text changed to "Exporting Spreadsheet In Progress".
- If exporting spreadsheet successful, status bar colour changed to "completed", status bar text changed to "Exporting Spreadsheet Completed Successfully".

7. UI returned to "ready" state. END.

### Exception: failed to save to records:
6.1. Status bar colour changed to "error". Status bar text changed to "Error Saving to Records". UI remains in current state, user can make another attempt. END.

### Exception: failed to save spreadsheet:
6.1. Status bar colour changed to "error". Status bar text changed to "Error Exporting Spreadsheet". UI returned to "ready" state. END.

### Variation: user clicks "Save and Email" button:
6.1. TODO

### Variation: user cancels creating new invoice
1.1. At any point, the user may abort this procedure by clicking the "Cancel" button. The window returns to the "ready" state.
