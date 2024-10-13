using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace PastebinPage
{
    public class Pastebin
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Pastebin(IWebDriver driver)
        {
            this.driver = driver;
            driver.Navigate().GoToUrl("https://pastebin.com/");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.25)
            };
        }

        public string[] InputInfo(string code, string name)
        {
            wait.Until(d => d.FindElement(By.Id("postform-text")).Displayed);
            var codeField = driver.FindElement(By.Id("postform-text"));
            codeField.SendKeys(code);

            wait.Until(d => 
            {
                var element = d.FindElement(By.Id("select2-postform-format-container"));
                return element != null && element.Enabled && element.Displayed;
            });

            var syntaxHighlightDropdown = driver.FindElement(By.Id("select2-postform-format-container"));
            syntaxHighlightDropdown.Click();

            wait.Until(d => d.FindElement(By.ClassName("select2-search__field")).Displayed);
            var syntaxSearchField = driver.FindElement(By.ClassName("select2-search__field"));
            syntaxSearchField.SendKeys("Bash");
            syntaxSearchField.SendKeys(Keys.Enter);

            wait.Until(d => 
            {
                var element = d.FindElement(By.Id("select2-postform-expiration-container"));
                return element != null && element.Enabled && element.Displayed;
            });

            var expirationDropdown = driver.FindElement(By.Id("select2-postform-expiration-container"));
            expirationDropdown.Click();

            wait.Until(d => d.FindElement(By.XPath("//li[contains(text(), '10 Minutes')]")).Displayed);
            var tenMinOption = driver.FindElement(By.XPath("//li[contains(text(), '10 Minutes')]"));
            tenMinOption.Click();

            var nameInput = driver.FindElement(By.Id("postform-name"));
            nameInput.SendKeys(name);

            var submitBtn = driver.FindElement(By.XPath("//button[contains(text(), 'Create New Paste')]"));
            submitBtn.Click();

            wait.Until(d => d.FindElement(By.XPath("//div[@class='info-top']//h1")).Displayed);
            var title = driver.FindElement(By.XPath("//div[@class='info-top']//h1")).Text;

            var syntax = driver.FindElement(By.XPath("//a[contains(@class, 'btn') and contains(text(), 'Bash')]")).Text;

            var codeRes = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/div[4]/div[2]/ol/li/div")).Text;

            return new string[] { title, syntax, codeRes };
        }
    }
}