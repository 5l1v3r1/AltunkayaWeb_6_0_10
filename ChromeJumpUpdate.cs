using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Reflection;

namespace AltunkayaWeb_6_0_10
{
    public class ChromeJumpUpdate
    {
        public static void CloseChromeDriver()
        {
            try
            {
                foreach (var processs in Process.GetProcessesByName("chromedriver.exe"))
                {
                    processs.Kill(true);
                }
                //Runtime.getRuntime().exec("taskkill /F /IM geckodriver.exe /T");
                //Runtime.getRuntime().exec("taskkill /F /IM chromedriver.exe /T");
                //Runtime.getRuntime().exec("taskkill /F /IM IEDriverServer.exe /T");
                Process process = new();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C taskkill /F /IM chromedriver.exe /T ";
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

        // ikinci çözüm
        public void DownloadLatestVersionOfChromeDriver()
        {
            string path = DownloadLatestVersionOfChromeDriverGetVersionPath();
            var version = DownloadLatestVersionOfChromeDriverGetChromeVersion(path);
            var urlToDownload = DownloadLatestVersionOfChromeDriverGetURLToDownload(version);
            DownloadLatestVersionOfChromeDriverKillAllChromeDriverProcesses();
            //CloseChromeDriver();
            DownloadLatestVersionOfChromeDriverDownloadNewVersionOfChrome(urlToDownload);
        }

        public string DownloadLatestVersionOfChromeDriverGetVersionPath()
        {
            //Path originates from here: https://chromedriver.chromium.org/downloads/version-selection            
            using RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe");
            if (key != null)
            {
                Object o = key.GetValue("");
                if (!String.IsNullOrEmpty(o.ToString()))
                {
                    return o.ToString();
                }
                else
                {
                    throw new ArgumentException("Unable to get version because chrome registry value was null");
                }
            }
            else
            {
                throw new ArgumentException("Unable to get version because chrome registry path was null");
            }
        }

        public string DownloadLatestVersionOfChromeDriverGetChromeVersion(string productVersionPath)
        {
            if (String.IsNullOrEmpty(productVersionPath))
            {
                throw new ArgumentException("Unable to get version because path is empty");
            }

            if (!File.Exists(productVersionPath))
            {
                throw new FileNotFoundException("Unable to get version because path specifies a file that does not exists");
            }

            var versionInfo = FileVersionInfo.GetVersionInfo(productVersionPath);
            if (versionInfo != null && !String.IsNullOrEmpty(versionInfo.FileVersion))
            {
                return versionInfo.FileVersion;
            }
            else
            {
                throw new ArgumentException("Unable to get version from path because the version is either null or empty: " + productVersionPath);
            }
        }

        public string DownloadLatestVersionOfChromeDriverGetURLToDownload(string version)
        {
            if (String.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Unable to get url because version is empty");
            }

            //URL's originates from here: https://chromedriver.chromium.org/downloads/version-selection
            string html = string.Empty;
            string urlToPathLocation = @"https://chromedriver.storage.googleapis.com/LATEST_RELEASE_" + String.Join(".", version.Split('.').Take(3));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPathLocation);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            if (String.IsNullOrEmpty(html))
            {
                throw new WebException("Unable to get version path from website");
            }

            return "https://chromedriver.storage.googleapis.com/" + html + "/chromedriver_win32.zip";
        }

        public void DownloadLatestVersionOfChromeDriverKillAllChromeDriverProcesses()
        {
            //It's important to kill all processes before attempting to replace the chrome driver, because if you do not you may still have file locks left over
            var processes = Process.GetProcessesByName("chromedriver");
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch
                {
                    //We do our best here but if another user account is running the chrome driver we may not be able to kill it unless we run from a elevated user account + various other reasons we don't care about
                }
            }
        }

        public void DownloadLatestVersionOfChromeDriverDownloadNewVersionOfChrome(string urlToDownload)
        {
            if (String.IsNullOrEmpty(urlToDownload))
            {
                throw new ArgumentException("urlToDownload boş olduğundan url alınamıyor");
            }

            //İndirilen dosyalar her zaman zip olarak gelir, her şeyi doğru yere getirmek için biraz değişiklik yapmamız gerekiyor
            using (var client = new WebClient())
            {
                if (File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip"))
                {
                    File.Delete(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip");
                }

                client.DownloadFile(urlToDownload, "chromedriver.zip");

                if (File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip") && File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.exe"))
                {
                    //CloseChromeDriver();
                    File.Delete(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.exe");
                }

                if (File.Exists(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip"))
                {
                    System.IO.Compression.ZipFile.ExtractToDirectory(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip", System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
                }
            }
            //CloseChromeDriver();
        }
    }
}