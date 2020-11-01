using System;
using System.IO;

namespace virusTotalCmdline.Utils
{

    public class UserApiMgmt
    {
        public string ApiKey { get; private set; }

        private const string TOKENDIR = @"\.virusTotal";
        private const string TOKEN = "token";

        public string HomeDirectory => Environment.OSVersion.Platform ==  PlatformID.Unix? Environment.GetEnvironmentVariable("HOME") : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        public string TokenDirectory => HomeDirectory + TOKENDIR;
        public string TokenFile => TokenDirectory + "\\" + TOKEN;

        public UserApiMgmt(string apiKey = null)
        {
            if (apiKey == null)
            {
                if (!TokenExists())
                {
                    throw new System.InvalidOperationException("Api Key has not been passed and API token file does not exist");
                }
                else
                {
                    ApiKey = File.ReadAllText(TokenFile);
                }
            }
            else
            {
                Directory.CreateDirectory(TokenDirectory);
                File.WriteAllText(TokenFile, apiKey);
                ApiKey = apiKey;
            }
        }

        private bool TokenExists()
        {
            return (File.Exists(TokenFile));
        }
    }
}
