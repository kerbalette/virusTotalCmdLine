using System;
using System.Collections.Generic;
using System.Text;
using virusTotalCmdline.Models;

namespace virusTotalCmdline.Events
{
    public class FileReportEventArgs : EventArgs
    {
        public FileReportEventArgs(FileReportResponse fileReportResponse)
        {
            FileReportResponse = fileReportResponse;
        }

        public FileReportResponse FileReportResponse { get; set; }
    }

    
}
