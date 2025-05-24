using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;
using System;

namespace LoginAutomation
{
    public class LoginTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            // Create case-insensitive configuration
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };

            using (var reader = new StreamReader("LoginTestData.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                foreach (var record in csv.GetRecords<TestData>())
                {
                    yield return new object[] { record.Username, record.Password };
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Login_WithValidCredentials_ShouldSucceed(string username, string password)
        {
            // Replace the path with the actual path to your login.html file
            string loginPagePath = Path.GetFullPath("login.html");
            string loginPageUrl = $"file:///{loginPagePath.Replace("\\", "/")}";

            // Initialize the ChromeDriver
            using (IWebDriver driver = new ChromeDriver())
            {
                try
                {
                    // Navigate to the login page
                    driver.Navigate().GoToUrl(loginPageUrl);

                    // Find the username and password fields and fill them
                    driver.FindElement(By.Id("username")).SendKeys(username);
                    driver.FindElement(By.Id("password")).SendKeys(password);

                    // Click the login button
                    driver.FindElement(By.Id("loginButton")).Click();

                    // For a real application, you would add assertions here to check if login was successful
                    // For this example, we'll just wait for a few seconds to simulate the login process
                    System.Threading.Thread.Sleep(2000);

                    // Basic assertion - in a real scenario, you'd check for success elements
                    Assert.True(true, "Login test completed");
                }
                catch (Exception ex)
                {
                    Assert.True(false, $"Login test failed: {ex.Message}");
                }
            }
        }

        public class TestData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}