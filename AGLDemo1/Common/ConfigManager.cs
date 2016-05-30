using System;
using System.Configuration;
using Serilog;

namespace AGLPetApiConsumer.Common
{
    public static class ConfigManager
    {
        public static string GetConfigSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                return appSettings[key];
            }
            catch (ConfigurationErrorsException ex)
            {
                Log.Logger.Error(ex, @"Error: AGLPetApiConsumer. Error reading app settings. Could not find value for the key: {key}", key);

                throw;
            }
        }
    }
}