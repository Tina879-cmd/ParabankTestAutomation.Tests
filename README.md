# Parabank Test Automation

Automated end-to-end testing for the Parabank web application using .NET 10, Playwright, Reqnroll (BDD), and GitHub Actions CI/CD. This framework is designed for maintainability, scalability, and robust reporting.

---

## Features

- **BDD with Gherkin**: Write tests in plain English using Reqnroll.
- **Playwright Automation**: Fast, reliable browser automation supporting Chromium, Firefox, and WebKit.
- **Strongly-Typed Configuration**: Centralized settings via `appsettings.json` and C# POCOs.
- **Comprehensive Logging**: Trace, Info, and Error logs with screenshots for failures.
- **Playwright HTML Reporting**: Rich HTML trace and report generation for every test run.
- **Page Object Model**: Clean separation of UI logic for maintainable tests.
- **FluentAssertions**: Readable, expressive assertions.
- **CI/CD with GitHub Actions**: Automated build, test, and artifact upload on every push and pull request.
- **Code Quality**: Enforced with StyleCop and `.editorconfig`.

## Setup

1. **Install .NET 10 SDK**  
   [Download .NET 10](https://dotnet.microsoft.com/download/dotnet/10.0)

2. **Install Playwright Browsers**  
   Run:  
   ```sh
   dotnet tool install --global Microsoft.Playwright.CLI
   playwright install

3. **Restore .NET dependencies**
    Run:  
   ```sh
   dotnet restore

4. **Configure settings**
   - Edit appsettings.json to set the base URL, credentials, and other environment-specific values.

## Running Tests

1. **Run all tests**
    Run:  
   ```sh
   dotnet test

2. **View Playwright HTML report**
   - After test execution, locate the Playwright trace or HTML report directory (commonly /TestResults or /traces).
   - Open the HTML report in your browser: npx playwright show-trace "<path-to-trace.zip>"

## Continuous Integration

1. **GitHub Actions**
   Every push or pull request to the main branch triggers the workflow defined in .github/workflows/ci.yml.
2. **Artifacts**
   Test results and Playwright traces are uploaded and available for download from the workflow run page.

## Project Structure

```
ParabankTestAutomation/
├── Features/         # Gherkin feature files
├── Steps/            # Step definitions (Reqnroll)
├── Pages/            # Page Object Model classes
├── Config/           # Configuration and strongly-typed settings
├── Logs/             # Test logs and screenshots
├── .github/workflows/ci.yml  # GitHub Actions workflow
├── appsettings.json  # Project configuration
└── README.md         # Project documentation
```
