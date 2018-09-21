﻿using Lykke.SettingsReader.Attributes;

namespace Lykke.Job.FxPriceAdapter.Settings.JobSettings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }
    }
}
