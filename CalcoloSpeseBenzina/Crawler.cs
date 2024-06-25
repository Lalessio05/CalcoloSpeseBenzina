using CalcoloSpeseBenzina.Components;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CalcoloSpeseBenzina;

internal static class Crawler   //Va fatto diventare un singleton
{
    public static (double benzina, double gasolio) CheckPrices()
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
        var cells = row.FindElements(By.TagName("td")).Select(x => x.Text.Replace('.', ',')).ToList();
        var benzina = double.Parse(cells[2]);
        var diesel = double.Parse(cells[1]);
        return (benzina, diesel);
    }

    public static void StartCrawling(List<Percorso> percorsi)
    {
        using WebDriver crawler = new ChromeDriver();
        WebDriverWait wait = new(crawler, TimeSpan.FromSeconds(10));
        crawler.Navigate().GoToUrl("https://www.google.com/maps");
        var cookiesButton = crawler.FindElement(By.XPath("(.//span[text()='Accetta tutto'])[1]"));
        wait.Until(_=>cookiesButton.Enabled);
        cookiesButton.Click();
        var searchBar = crawler.FindElement(By.XPath(".//input[1]"));
        wait.Until(_ => searchBar.Enabled);
        foreach (var percorso in percorsi)
        {
            searchBar.SendKeys(percorso.Partenza);
            var result = crawler.FindElement(By.XPath(".//input[1]/../../following-sibling::*[1]"));
            wait.Until(_ => result.Enabled);
            result.Click();
        }
    }
}