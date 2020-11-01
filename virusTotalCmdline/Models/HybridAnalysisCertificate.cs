using System;
using System.Collections.Generic;
using System.Text;

namespace virusTotalCmdline.Models
{
    public class HybridAnalysisCertificate
    {
        public string Owner { get; set; }
        public string Issuer { get; set; }
        public string Serial_Number { get; set; }
        public string MD5 { get; set; }
        public string Sha1 { get; set; }
        public string Valid_From { get; set; }
        public string Valid_To { get; set; }
    }
}
