using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using virusTotalCmdline.Events;
using virusTotalCmdline.Models;
using virusTotalCmdline.Utils;

namespace virusTotalCmdline.Processors
{
    public class VirusTotalProcessor
    {
        public event EventHandler<FileReportEventArgs> FileReportPerformed;
        public string ApiKey { get; private set; }

        private FileReportResponse fileReportModel;

        public VirusTotalProcessor(string apiKey)
        {
            ApiKey = apiKey;
        }

        public async Task<FileReportResponse> FileReport(string hash)
        {
            string url = $"https://www.virustotal.com/vtapi/v2/file/report?apikey={ApiKey}&resource={hash}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    fileReportModel = await response.Content.ReadAsAsync<FileReportResponse>();


                    var completed = FileReportPerformed as EventHandler<FileReportEventArgs>;
                    if (completed != null)
                        completed(this, new FileReportEventArgs(fileReportModel));

                    return fileReportModel;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public List<ScanResults> DetectedScans()
        {
            List<ScanResults> detectedScans = new List<ScanResults>();

            foreach (var item in fileReportModel.scans)
            {
                if (item.Value.Detected)
                    detectedScans.Add(new ScanResults(item.Key, item.Value.Detected, item.Value.Version, item.Value.Result, item.Value.Update));
            }

            // Linq method of only showing the detected results
            //List<ScanResults> detectedScans = fileReportModel.scans.Values.ToList<ScanResults>().Where(x => x.Detected).ToList();

            return detectedScans;
        }
    }
}