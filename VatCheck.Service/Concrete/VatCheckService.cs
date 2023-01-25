using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatCheck.API.Helpers;
using VatCheck.Service.Abstract;

namespace VatCheck.Service.Concrete
{
    public class VatCheckService : IVatCheckService
    {
        private readonly IOptions<AppSettings> appSettings;

        public VatCheckService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings;
        }

        public async Task<string> CheckVat(string vatNumber)
        {
            string resultDirty = await CheckVatOnIbanDotCom(vatNumber);

            string result = WhiteSpaceHelper.NormalizeWhiteSpace(resultDirty.Replace("\n", ""));

            return result;
        }

        private async Task<string> CheckVatOnIbanDotCom(string vatNumber)
        {
            string url = appSettings.Value.VatCheckUrl;
            HttpClient client = new HttpClient();
            HtmlDocument htmlDoc = new HtmlDocument();

            var content = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("vat_id", vatNumber)
            });

            var response = client.PostAsync(url, content).Result;

            var responseString = await response.Content.ReadAsStringAsync();            

            htmlDoc.LoadHtml(responseString);

            var tableNode = htmlDoc.DocumentNode.SelectSingleNode("//table[@class='table table-bordered downloads']//tr//th");

            string resultDirty = tableNode.InnerText;

            return resultDirty;
        }
    }
}
