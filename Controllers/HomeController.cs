using AltunkayaWeb_6_0_10.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace AltunkayaWeb_6_0_10.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //private static WebDriver OpenDriver(WebDriver? webDriver) // Geri alınacak odunu keser nacak
        //{
        //    if (webDriver == null)
        //    {
        //        ChromeDriverService service = ChromeDriverService.CreateDefaultService();
        //        service.HideCommandPromptWindow = true;
        //        ChromeOptions options = new();
        //        options.AddArguments("--headless");
        //        try
        //        {
        //            driver = new ChromeDriver(service, options);
        //            driver.Manage().Window.Maximize();
        //        }
        //        catch
        //        {
        //            driver = new ChromeDriver();
        //            driver.Manage().Window.Maximize();
        //        }

        //        return driver;
        //    }
        //    return webDriver;
        //}

        //public async Task<IActionResult> GetDoviz()
        //{
        //    var url = "https://m.carsidoviz.com/";
        //    var httpClient = new HttpClient();
        //    var response = await httpClient.GetStringAsync(url);
        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(response);
        //    Doviz doviz = new()
        //    {
        //        DolarAlis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[1]/div[2]/div/div[2]/div[2]/div/div[1]")?.InnerText,
        //        DolarSatis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[1]/div[2]/div/div[2]/div[2]/div/div[2]")?.InnerText,
        //        EuroAlis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[2]/div[2]/div/div[2]/div[2]/div/div[1]")?.InnerText,
        //        EuroSatis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[2]/div[2]/div/div[2]/div[2]/div/div[2]")?.InnerText
        //    };

        //    return Json(doviz);

        //    //iframe CSS Selector
        //    //"#carsidoviz"

        //    //innerHTML
        //    //"18.6700"

        //    //OuterHTML
        //    //"<div class=\"col-6 fiyat ayni\">18.6700</div>"

        //    //CSS Selector
        //    //"div.ayni:nth-child(1)"

        //    //CSS Path
        //    //"html body div#__nuxt div#__layout div#app.v-application.v-application--is-ltr.theme--light div.v-application--wrap main.v-main div.v-main__wrap div.container div.row.align-center.justify-center div.col-sm-12.col-md-12.col-12 div.text-center div.v-card.v-sheet.v-sheet--outlined.theme--light.elevation-4 div.v-card__text div.row div.col-9 div.col-12 div.row div.col-6.fiyat.ayni"
        //    //"/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[1]/div[2]/div/div[2]/div[2]/div/div[1]"

        //    //XPath
        //    //"/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[1]/div[2]/div/div[2]/div[2]/div/div[1]"
        //    // --------------------------------------
        //    // --------------------------------------
        //    // --------------------------------------
        //    //var url = "https://m.carsidoviz.com/";
        //    //var httpClient = new HttpClient();
        //    //var response = await httpClient.GetStringAsync(url);
        //    //var doc = new HtmlDocument();
        //    //doc.LoadHtml(response);
        //    //Doviz doviz = new()
        //    //{
        //    //    DolarAlis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[1]/div[2]/div/div[2]/div[2]/div/div[1]")?.InnerText,
        //    //    DolarSatis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[1]/div[2]/div/div[2]/div[2]/div/div[2]")?.InnerText,
        //    //    EuroAlis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[2]/div[2]/div/div[2]/div[2]/div/div[1]")?.InnerText,
        //    //    EuroSatis = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div/main/div/div/div/div[1]/div/div[2]/div[2]/div/div[2]/div[2]/div/div[2]")?.InnerText
        //    //};

        //    //return Json(doviz);
        //}

        public IActionResult IframeDoviz()
        {
            return View();
        }

        public async Task<IActionResult> GetAltin()
        {
            var url = "https://www.malatyakuyumcular.com/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            HtmlNode  node = doc.DocumentNode.SelectSingleNode(xpath: "//html/body/table");

            Altin altin = new()
            {
                Liralik_Eski_Alis = node.InnerText.Split('\n')[32].Trim().Replace('\r','\0'),
                //Liralik_Eski_Alis = doc.DocumentNode.ChildNodes.FirstOrDefault(x => x.XPath == "/html/body/table/tbody/tr[7]/td[2]/div/strong").InnerText,
                Liralik_Eski_Satis = node.InnerText.Split('\n')[33].Trim().Replace('\r', '\0'),
                Cumhuriyet_Ceyrak_Eski_Alis = node.InnerText.Split('\n')[71].Trim().Replace('\r', '\0'),
                Cumhuriyet_Ceyrak_Eski_Satis = node.InnerText.Split('\n')[72].Trim().Replace('\r', '\0'),
                Cumhuriyet_Yarim_Eski_Alis = node.InnerText.Split('\n')[59].Trim().Replace('\r', '\0'),
                Cumhuriyet_Yarim_Eski_Satis = node.InnerText.Split('\n')[60].Trim().Replace('\r', '\0'),
                Y22_Ayar_Bilezik_Alis = node.InnerText.Split('\n')[81].Trim().Replace('\r', '\0'),
                //"/html/body/table/tbody/tr[16]/td[3]/div/strong"
                Y22_Ayar_Bilezik_Satis = node.InnerText.Split('\n')[82].Trim().Replace('\r', '\0'),
            };

            return Json(altin);
        }

        //private IActionResult CloseDrive()
        //{
        //    //driver?.Quit(); // Geri alınacak odunu keser nacak
        //    CloseExe("chromedriver.exe");
        //    CloseExe("chrome.exe");
        //    try
        //    {
        //        RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //    }
        //    return Json("");
        //}

        public void CloseExe(string exe)
        {
            try
            {
                foreach (var processs in Process.GetProcessesByName(exe))
                {
                    processs.Kill(true);
                }
                //Runtime.getRuntime().exec("taskkill /F /IM geckodriver.exe /T");
                //Runtime.getRuntime().exec("taskkill /F /IM chromedriver.exe /T");
                //Runtime.getRuntime().exec("taskkill /F /IM IEDriverServer.exe /T");
                Process process = new();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C taskkill /F /IM " + exe + " /T ";
                process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception)
            {
                string metin = "ChromeDriver kapatılamadı..";
                Console.WriteLine(metin, Color.Red);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public HtmlDocument HtmlDocument(string html)
        {
            var d = new HtmlDocument();
            d.LoadHtml(html);

            return d;
        }

        //public async Task<string> ParseHtmlAsync(string tenant, string html)
        //{
        //    var doc = new HtmlDocument();
        //    doc.LoadHtml(html);

        //    var nodes = doc.DocumentNode.SelectNodes("//include[@article-alias and @category-alias]");
        //    if (nodes == null)
        //    {
        //        return html;
        //    }

        //    foreach (var node in nodes)
        //    {
        //        string alias = node.Attributes["article-alias"].Value;
        //        string categoryAlias = node.Attributes["category-alias"].Value;

        //        var model = await ContentModel.GetContentAsync(tenant, categoryAlias, alias).ConfigureAwait(false);
        //        if (model != null)
        //        {
        //            string contents = model.Contents;

        //            var newNode = HtmlNode.CreateNode(contents);
        //            node.ParentNode.ReplaceChild(newNode, node);
        //        }
        //    }

        //    return doc.DocumentNode.OuterHtml;
        //}

        //private IWebDriver OpenDriverAltin()
        //{
        //    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
        //    service.HideCommandPromptWindow = true;
        //    ChromeOptions options = new();
        //    options.AddArguments("--headless");
        //    var driver = new ChromeDriver(service, options);
        //    driver.Manage().Window.Maximize();
        //    return driver;
        //}

        //private IWebDriver OpenDriverDoviz()
        //{
        //    ChromeDriverService service = ChromeDriverService.CreateDefaultService();
        //    service.HideCommandPromptWindow = true;
        //    ChromeOptions options = new();
        //    options.AddArguments("--headless");
        //    if (driverDoviz == null)
        //    {
        //        ChromeDriver driver = new(service, options);
        //        driver.Manage().Window.Maximize();
        //        return driver;
        //    }
        //    return driverDoviz;
        //}

        //public async Task<string> GetSymbolValue(string symbol = "BTCTRY")
        //{
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync("https://api.binance.com/api/v3/ticker/bookTicker?symbol=" + symbol);
        //    response.EnsureSuccessStatusCode();
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<EnstrumanPrice>(responseBody);
        //    //alicinin bir hisse / doviz icin odeyecegi en yuksek degeri ifade eden "bid price"(satin alma teklifi fiyati) ile, saticinin bir hisseyi / dovizi satmak icin razi olacagi en dusuk fiyati ifade eden "ask price" arasindaki fark. 
        //    return result.askPrice;
        //}

        //public async Task<string> GetBTCTRY()
        //{
        //    HttpClient client = new();
        //    HttpResponseMessage response = await client.GetAsync("https://api.binance.com/api/v3/ticker/bookTicker?symbol=BTCTRY");
        //    response.EnsureSuccessStatusCode();
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<EnstrumanPrice>(responseBody);

        //    return result.askPrice;
        //}

        //public async Task<string> GetETHTRY()
        //{
        //    HttpClient client = new();
        //    HttpResponseMessage response = await client.GetAsync("https://api.binance.com/api/v3/ticker/bookTicker?symbol=ETHTRY");
        //    response.EnsureSuccessStatusCode();
        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<EnstrumanPrice>(responseBody);

        //    return result.askPrice;
        //}

        //public Crypto GetCrypto()
        //{
        //    Crypto crypto = new();
        //    driver.Navigate().GoToUrl(cryptoUrl + "symbols/BTCTRY/");
        //    Thread.Sleep(500);
        //    crypto.BTC = driver.FindElement(By.XPath("/html/body/div[3]/div[4]/div[2]/header/div/div[3]/div[1]/div/div/div/div[1]/div[1]")).Text;
        //    driver.Navigate().GoToUrl(cryptoUrl + "symbols/ETHTRY/");
        //    Thread.Sleep(500);
        //    crypto.ETH = driver.FindElement(By.XPath("//*[@id=\"anchor-page-1\"]/div/div[3]/div[1]/div/div/div/div[1]/div[1]")).Text +
        //         driver.FindElement(By.XPath("//*[@id=\"anchor-page-1\"]/div/div[3]/div[1]/div/div/div/div[1]/div[1]/span")).Text;
        //    return crypto;
        //}

        //public IActionResult GetPiyasa()
        //{
        //    Altin altin;
        //    Doviz doviz;
        //    try
        //    {
        //        altin = GetAltin();
        //        doviz = GetDoviz();
        //    }
        //    catch (System.Exception)
        //    {
        //        if (driver != null)
        //        {
        //            driver?.Quit();
        //            ChromeJumpUpdate.CloseChromeDriver();
        //        }
        //        return RedirectToAction("index");
        //    }

        //    Piyasa piyasa = new()
        //    {
        //        Altin = altin,
        //        Doviz = doviz
        //    };


        //    return Json(piyasa);
        //}

        //private void UpdateChrome()
        //{
        //    try
        //    {
        //        //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
        //        //service.HideCommandPromptWindow = true;
        //        //ChromeOptions options = new();
        //        //options.AddArguments("--headless");
        //        //webDriver = new ChromeDriver(service, options);
        //        driver.Navigate().GoToUrl("www.google.com");
        //    }
        //    catch
        //    {
        //        if (driver != null)
        //        {
        //            driver?.Quit();
        //            ChromeJumpUpdate.CloseChromeDriver();
        //        }

        //        var chromeJumpUpdate = new ChromeJumpUpdate();
        //        chromeJumpUpdate.DownloadLatestVersionOfChromeDriver();
        //    }
        //    finally
        //    {
        //        if (driver != null)
        //        {
        //            driver?.Quit();
        //            ChromeJumpUpdate.CloseChromeDriver();
        //        }
        //    }
        //}




    }
}
