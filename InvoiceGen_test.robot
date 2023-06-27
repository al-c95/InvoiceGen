*** Settings ***
Library     AutoItLibrary

*** Variables ***
${APP}  InvoiceGen.exe
${WORKING_DIR}  ${EMPTY}
${SW_SHOW}  5

*** Test Cases ***
TC0
    Run     ${APP}  ${WORKING_DIR}    ${SW_SHOW}
    BuiltIn.Sleep   2s  