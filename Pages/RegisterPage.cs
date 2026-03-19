using Microsoft.Playwright;
using ParabankTestAutomation.Tests.Config;

namespace ParabankTestAutomation.Tests.Pages
{
    public class RegisterPage : BasePage
    {
        // Locators
         public string RegisterLink => "a[href='register.htm']";
        public string Firstname => "#customer\\.firstName";
        public string Lastname => "#customer\\.lastName";
        public string Address => "#customer\\.address\\.street";
        public string City => "#customer\\.address\\.city";
        public string State => "#customer\\.address\\.state";
        public string ZipCode => "#customer\\.address\\.zipCode";
        public string PhoneNumber => "#customer\\.phoneNumber";
        public string SSN => "#customer\\.ssn";
        public string Username => "#customer\\.username";
        public string Password => "#customer\\.password";
        public string ConfirmPassword => "#repeatedPassword";
        public string RegisterButton => "input[value='Register']";

        public RegisterPage(IPage page)
            : base(page) {}

        public async Task ClickOnRegisterLink()
        {
            await WaitForVisibleAsync(RegisterLink);
            await ClickAsync(RegisterLink);
        }

        public async Task FillRegistrationForm(string firstName, string lastName, string address, string city, string state, string zipCode, string phoneNumber, string ssn, string username, string password)
        {
            await WaitForVisibleAsync(Firstname);
            await FillAsync(Firstname, firstName);
            await WaitForVisibleAsync(Lastname);
            await FillAsync(Lastname, lastName);
            await WaitForVisibleAsync(Address);
            await FillAsync(Address, address);
            await WaitForVisibleAsync(City);
            await FillAsync(City, city);
            await WaitForVisibleAsync(State);
            await FillAsync(State, state);
            await WaitForVisibleAsync(ZipCode);
            await FillAsync(ZipCode, zipCode);
            await WaitForVisibleAsync(PhoneNumber);
            await FillAsync(PhoneNumber, phoneNumber);
            await WaitForVisibleAsync(SSN);
            await FillAsync(SSN, ssn);
            await WaitForVisibleAsync(Username);
            await FillAsync(Username, username);
            await WaitForVisibleAsync(Password);
            await FillAsync(Password, password);
            await WaitForVisibleAsync(ConfirmPassword);
            await FillAsync(ConfirmPassword, password);
        }

        public async Task ClickOnRegisterButton()
        {
            await WaitForVisibleAsync(RegisterButton);
            await ClickAsync(RegisterButton);
        }
    }
}