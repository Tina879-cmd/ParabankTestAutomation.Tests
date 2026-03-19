Feature: Products
  As a Parabank user
  I want to view available products
  So that I can see what services the bank offers

  Scenario: View all products
    Given the user navigates to the Parabank application
    When the user navigates to the Products page
    Then the user should be able to view the title "PRODUCTS"
    And the user should be able to view the list of available products

  Scenario: View product details
    Given the user navigates to the Parabank application
    When the user navigates to the Products page
    And the user clicks on Functional Testing button
    And the user clicks on the SOAtest product
    Then the user should be able to view the title "Parasoft SOAtest"