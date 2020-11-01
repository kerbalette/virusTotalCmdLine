using System.Collections.Generic;

namespace virusTotalCmdline
{
    class ApplicationArguments
    {
        public string apikey { get; set; }
        public List<string> filenames { get; set; }
        public List<string> hashes { get; set; }

    }
}
