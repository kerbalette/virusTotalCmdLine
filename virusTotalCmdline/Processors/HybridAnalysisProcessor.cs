using System;
using System.Collections.Generic;
using System.Text;
using virusTotalCmdline.Models;
using virusTotalCmdline.Utils;
using System.Net.Http;
using System.Threading.Tasks;

namespace virusTotalCmdline.Processors
{
    public class HybridAnalysisProcessor
    {
        public string ApiKey { get; set; }

        public HybridAnalysisProcessor(string apiKey)
        {
            ApiKey = apiKey;
        }

        //public async Task<HybridAnalysisHash> PostSearchHash(string hash)
        //{
        //    ApiHelper.ApiClient.DefaultRequestHeaders.Add("api-key", ApiKey);
        //    string url = $"www.hybrid-analysis.com/api/v2/search/hashes";
        //    await ApiHelper.ApiClient.PostAsync()


        //}
    }
}
