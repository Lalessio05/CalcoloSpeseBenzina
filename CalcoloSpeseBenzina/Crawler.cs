using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CalcoloSpeseBenzina;

internal static class Crawler
{
    public static (double benzina,double gasolio) CheckPrices()
    {
        const string xpathLombardia = @".//td[text()='Lombardia']/..";
        using WebDriver crawler = new ChromeDriver();
        WebDriverWait wait = new(crawler, TimeSpan.FromSeconds(10));
        crawler.Navigate().GoToUrl("https://icadsistemi.com/prezzo-medio-regionale-carburanti");
        var lombardiaRow = crawler.FindElement(By.XPath(xpathLombardia));
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathLombardia)));
        return GetPrices(lombardiaRow); 
    }
    private static (double, double) GetPrices(IWebElement row)
    {
        var cells = row.FindElements(By.TagName("td"));
        double benzina = double.Parse(cells[2].Text);
        double diesel = double.Parse(cells[1].Text);
        return (benzina, diesel);
    }
}