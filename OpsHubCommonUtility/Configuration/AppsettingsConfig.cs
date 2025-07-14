using System;
using System.Collections.Generic;
using System.Text;

namespace OpsHubCommonUtility.Configuration
{
    public class AppsettingsConfig
    {
        public OpsHubData OpsHubData { get; set; } = new OpsHubData();
        public EmailSetting EmailSetting { get; set; } = new EmailSetting();
        public bool RequestResponseLoggingEnabled { get; set; }
    }
}