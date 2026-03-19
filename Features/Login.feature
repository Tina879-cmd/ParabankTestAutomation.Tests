Feature: Login
  As a user I want to login into the Parabank application.

  Scenario: Successful login with valid credentials 
    Given the user navigates to the login page
    When the user logs in with valid credentials
    Then the user should view the title "Accounts Overview"

  Scenario: Login with invalid credentials
    Given the user navigates to the login page
    When the user logs in with invalid credentials
    Then the user should view an error message "The username and password could not be verified."
