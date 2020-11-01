using System;
using System.Collections.Generic;
using System.Text;

namespace virusTotalCmdline.Models
{
    public class HybridAnalysisHash
    {
        public string Job_Id { get; set; }
        public int Environment_Id { get; set; }
        public string Environment_Description { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Type_Short { get; set; }
        public string Target_Url { get; set; }
        public string State { get; set; }
        public string Error_Type { get; set; }
        public string Error_Origin { get; set; }
        public string MD5 { get; set; }
        public string Sha1 { get; set; }
        public string Sha256 { get; set; }
        public string Sha512 { get; set; }
        public string SsDeep { get; set; }
        public string ImpHash { get; set; }
        public int AV_Detect { get; set; }
        public string VX_Family { get; set; }
        public string Analysis_Start_Time { get; set; }
        public int Threat_Score { get; set; }
        public bool Interesting { get; set; }
        public int Threat_Level { get; set; }
        public string Verdict { get; set; }
        public Dictionary<string, HybridAnalysisCertificate> Certificates { get; set; }
        public List<string> Domains { get; set; }
        public List<string> Classification_Tags { get; set; }
        public List<string> Compromised_Hosts { get; set; }
        public List<string> Hosts { get; set; }
        public int Total_Network_Connections { get; set; }
        public int Total_Processess { get; set; }
        public int Total_Signatures { get; set; }

    }
}
