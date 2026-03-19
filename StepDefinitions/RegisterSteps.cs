using System.Threading.Tasks;
using Reqnroll;
using ParabankTestAutomation.Tests.Pages;
using ParabankTestAutomation.Tests.Utilities;
using FluentAssertions;

namespace ParabankTestAutomation.Tests.Steps
{
    [Binding]
    public class RegisterSteps : BaseTest
    {
        public RegisterSteps(ScenarioContext scenarioContext)
            : base(scenarioContext) {}

        [When("the user clicks on the Registration link")]
        public async Task ClickOnRegisterLink()
        {
            await PageObjects.RegisterPage.ClickOnRegisterLink();
        }    

        [When("the user fills registration fields")]
        public async Task FillRegistrationFields(Table table)
        {
            var row = table.Rows[0];
            await PageObjects.RegisterPage.FillRegistrationForm(
                row["First Name"],
                row["Last Name"],
                row["Address"],
                row["City"],
                row["State"],
                row["Zip Code"],
                row["Phone#"],
                row["SSN"],
                row["Username"],
                row["Password"]
            );
        }

        [When("the user clicks on the Register button")]
        public async Task ClickOnRegisterButton()
        {
            await PageObjects.RegisterPage.ClickOnRegisterButton();
        }
    }
}