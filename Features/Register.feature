Feature: Register
  As a user I want to register into the Parabank application to create an account.

  Scenario: Register a user to create an account
    Given the user navigates to the login page
    When the user clicks on the Registration link
    When the user fills registration fields
    |First Name |Last Name |Address     |City    |State |Zip Code |Phone#   |SSN          |Username   |Password |
    |May        |Doe       |125 Main St |Anytown |CA    |12445    |555-1244 |1235-45-6789 |MayDoeUser |Pwd@1234 |
    And the user clicks on the Register button
    Then the user should view the title "Welcome MayDoeUser"
