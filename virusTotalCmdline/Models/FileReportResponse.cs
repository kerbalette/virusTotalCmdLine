using System;
using System.Collections.Generic;
using System.Text;

namespace virusTotalCmdline.Models
{
    public class FileReportResponse
    {
        public int Response_Code { get; set; }
        public string Verbose_Msg { get; set; }
        public string Resource { get; set; }
        public string Scan_Id { get; set; }
        public string MD5 { get; set; }
        public string Sha1 { get; set; }
        public string Sha256 { get; set; }
        public DateTime Scan_Date { get; set; }
        public string Permalink { get; set; }
        public int Positives { get; set; }
        public int Total { get; set; }

        public Dictionary<string, ScanResults> scans { get; set; }
    }

    public class ScanResults
    {
        public ScanResults(string name, bool detected, string version, string result, string update)
        {
            Name = name;
            Detected = detected;
            Version = version;
            Result = result;
            Update = update;
        }

        public string Name { get; set; }
        public bool Detected { get; set; }
        public string Version { get; set; }
        public string Result { get; set; }
        public string Update { get; set; }
    }
}
