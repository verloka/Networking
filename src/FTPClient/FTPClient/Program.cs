using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await GetFolderContent("");
            await GetFolderContent("src/DNSTest");
            await DownloadFile();
            await UploadFile();

            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        public static async Task<string> GetFolderContent(string Path)
        {
            StringBuilder strBuilder = new StringBuilder();
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create($"ftp://localhost/{Path}");
            req.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            req.Credentials = new NetworkCredential("abc", "123");
            req.EnableSsl = false;

            FtpWebResponse resp = (FtpWebResponse)await req.GetResponseAsync();

            using (var respStream = resp.GetResponseStream())
            {
                using var reader = new StreamReader(respStream);
                strBuilder.Append(reader.ReadToEnd());
                strBuilder.Append(resp.WelcomeMessage);
                strBuilder.Append($"Request returned status:  {resp.StatusDescription}");
            }
            return strBuilder.ToString();
        }

        public static async Task<string> DownloadFile()
        {
            StringBuilder strBuilder = new StringBuilder();
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://localhost/src/Delta.cs");
            req.Method = WebRequestMethods.Ftp.DownloadFile;

            req.Credentials = new NetworkCredential("abc", "123");
            req.UsePassive = true;

            using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
            {

                using (var respStream = resp.GetResponseStream())
                {
                    strBuilder.Append(resp.StatusDescription);
                    if (!File.Exists(@"../Delta.cs"))
                        using var file = File.Create(@"../Delta.cs");

                    using var respReader = new StreamReader(respStream);
                    using var fileWriter = File.OpenWrite(@"../Delta.cs");
                    using var strWriter = new StreamWriter(fileWriter);
                    await strWriter.WriteAsync(respReader.ReadToEnd());
                }
            }

            return strBuilder.ToString();
        }

        public static async Task<string> UploadFile()
        {
            StringBuilder strBuilder = new StringBuilder();
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://localhost/Program.cs");
            req.Method = WebRequestMethods.Ftp.UploadFile;

            req.Credentials = new NetworkCredential("abc", "123");
            req.UsePassive = true;

            byte[] fileBytes;

            using (var reader = new StreamReader(@"Program.cs"))
                fileBytes = Encoding.ASCII.GetBytes(reader.ReadToEnd());

            req.ContentLength = fileBytes.Length;

            using (var reqStream = await req.GetRequestStreamAsync())
                await reqStream.WriteAsync(fileBytes, 0, fileBytes.Length);

            using (FtpWebResponse resp = (FtpWebResponse)req.GetResponse())
                strBuilder.Append(resp.StatusDescription);

            return strBuilder.ToString();
        }
    }
}
