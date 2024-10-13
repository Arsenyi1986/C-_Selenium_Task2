using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using PastebinPage;

namespace Tests;

public class Tests
{
    private IWebDriver? driver;

    [SetUp]
    public void Setup()
    {
        this.driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
    }

    [Test]
    public void Test1()
    {
        var title = "how to gain dominance among developers";
        var inpCode = @"git config --global user.name " + "New Sheriff in Town" + "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\ngit push origin master --force";
        Pastebin pb = new Pastebin(driver);
        var results = pb.InputInfo(inpCode, title);

        Assert.That(results[0], Is.EqualTo(title));
        Assert.That(results[1], Is.EqualTo("Bash"));
        Assert.That(results[2], Is.EqualTo(inpCode));
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}