using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Fclp;
using virusTotalCmdline.Models;
using virusTotalCmdline.Processors;
using virusTotalCmdline.Utils;
using System.Threading.Tasks;
using Fclp.Internals.Extensions;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace virusTotalCmdline
{
    class Program
    {
        private static Logger _logger;
        private static readonly string BaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static async Task Main(string[] args)
        {
            SetupNLog();
            _logger = LogManager.GetCurrentClassLogger();

            var cmdlineParser = new FluentCommandLineParser<ApplicationArguments>();
            List<FileMetaData> filesToBeProcessed = new List<FileMetaData>();

            cmdlineParser.Setup(arg => arg.apikey)
                .As('a', "apikey")
                .WithDescription("Use your API, if not supplied will try to use .virusTotal token file");

            cmdlineParser.Setup(arg => arg.filenames)
                .As('f', "filenames")
                .WithDescription("List of filenames seperated by a space to check against VirusTotal");

            cmdlineParser.Setup(arg => arg.hashes)
                .As('h', "hashes")
                .WithDescription("List of hashes seperated by a space to check against VirusTotal");

            var header =
                $"VTCmdLine version {Assembly.GetExecutingAssembly().GetName().Version}" +
                "\r\nAuthor: Andy Lockhart";

            var footer = @"Examples: VTCmdLine.exe -f ""C:\Windows\Explorer.exe""" + "\r\n\t ";


            cmdlineParser.SetupHelp("?", "help")
                .WithHeader(header)
                .Callback(text => _logger.Info(text + "\r\n" + footer));


            var parserResult = cmdlineParser.Parse(args);

            if (parserResult.HelpCalled)
                return;

            if (parserResult.HasErrors)
            {
                _logger.Error(parserResult.ErrorText);

                cmdlineParser.HelpOption.ShowHelp(cmdlineParser.Options);
                return;
            }

            if (UsefulExtension.IsNullOrEmpty(cmdlineParser.Object.filenames) &&
                UsefulExtension.IsNullOrEmpty(cmdlineParser.Object.hashes))
            {
                cmdlineParser.HelpOption.ShowHelp(cmdlineParser.Options);
                _logger.Warn("Either -f or -h is at least required. Exiting");
                return;
            }

            if (UsefulExtension.IsNullOrEmpty(cmdlineParser.Object.hashes) == false)
            {
                foreach (var hash in cmdlineParser.Object.hashes)
                {
                    if ((hash.Length != 20) && (hash.Length != 32) && (hash.Length != 64))
                    {
                        cmdlineParser.HelpOption.ShowHelp(cmdlineParser.Options);
                        _logger.Warn($"Invalid hash length passed for '{hash}'. Exiting");
                        return;
                    }
                    
                }
            }

            if (UsefulExtension.IsNullOrEmpty(cmdlineParser.Object.filenames) == false)
            {
                foreach (var file in cmdlineParser.Object.filenames)
                {
                    if (!File.Exists(file))
                    {
                        _logger.Warn($"File '{file}' not found. Exiting");
                        return;
                    }
                }
            }

            _logger.Info(header);
            _logger.Info($"Command line: {string.Join(" ", Environment.GetCommandLineArgs().Skip(1))}");

            ApiHelper.InitializeClient();

            UserApiMgmt userApiMgmt = new UserApiMgmt(cmdlineParser.Object.apikey);
            VirusTotalProcessor virusTotalProcess = new VirusTotalProcessor(userApiMgmt.ApiKey);
            virusTotalProcess.FileReportPerformed += VirusTotalProcess_FileReportPerformed;

            if (!parserResult.HasErrors)
            {
                if (cmdlineParser.Object.filenames != null)
                {
                    foreach (var filename in cmdlineParser.Object.filenames)
                    {
                        FileMetaData fileMetaData = new FileMetaData(filename);
                        _logger.Info($"\r\nCommencing Search on File: '{fileMetaData.FileName}'");
                        filesToBeProcessed.Add(fileMetaData);
                        _logger.Info($"MD5:\t\t\t{fileMetaData.MD5Hash}");
                        _logger.Info($"Sha1:\t\t\t{fileMetaData.SHA1Hash}");
                        _logger.Info($"Sha256:\t\t\t{fileMetaData.SHA256Hash}");
                        _logger.Info($"ImpHash:\t\t{fileMetaData.ImpHash}");

                        var fileReport = await virusTotalProcess.FileReport(fileMetaData.SHA256Hash);
                        DisplayDetectedScans(virusTotalProcess.DetectedScans());
                        if (fileReport.Positives == 0)
                            _logger.Info("No Malware Detected");

                        _logger.Info($"Scan Completed Successfully");
                    }
                }

                // Check if Hash also given
                List<string> hashes = cmdlineParser.Object.hashes;
                foreach (var item in hashes)
                {
                    _logger.Info($"\r\nCommencing Search on Hash: '{item}'");
                    FileMetaData fileMetaData = new FileMetaData(item);
                    var fileReport = await virusTotalProcess.FileReport(item);
                    DisplayDetectedScans(virusTotalProcess.DetectedScans());
                    if (fileReport.Positives == 0)
                        _logger.Info("No Malware Detected");

                    _logger.Info($"Scan Completed Successfully");
                }
            }
        }

        private static void DisplayDetectedScans(List<ScanResults> scanResults)
        {
            if (scanResults.Count > 0)
                _logger.Warn($"Malware Detected from the following sources");
            
            foreach(var item in scanResults)
                _logger.Info(item.Name + " - " + item.Result + " - " + item.Version);
        }

        private static void VirusTotalProcess_FileReportPerformed(object sender, Events.FileReportEventArgs e)
        {
           // WriteConsoleColour("Work Completed Successfully", ConsoleColor.Green);
           // Console.WriteLine("Work Completed Successfully");
            //Console.WriteLine(e.FileReportResponse.Verbose_Msg);
        }

        private static void SetupNLog()
        {
            if (File.Exists(Path.Combine(BaseDirectory, "NLog.config")))
                return;

            var config = new LoggingConfiguration();
            var logLevel = LogLevel.Info;

            var layout = @"${message}";
            var consoleTarget = new ColoredConsoleTarget();

            config.AddTarget("console", consoleTarget);
            consoleTarget.Layout = layout;

            var rule = new LoggingRule("*", logLevel, consoleTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;
        }
    }
}
