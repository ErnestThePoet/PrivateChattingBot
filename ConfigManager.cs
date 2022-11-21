﻿using Newtonsoft.Json;
using PrivateChattingBot.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace PrivateChattingBot
{
    internal class ConfigManager
    {
        private static BotConfig config;

        private const string JSON_FILE_PATH = "data/config.json";

        public static void Load()
        {
            string rawJson = "";
            try
            {
                rawJson = File.ReadAllText(
                    AppDomain.CurrentDomain.BaseDirectory + JSON_FILE_PATH);
            }
            catch
            {
                MessageBox.Show($"Failed to load {JSON_FILE_PATH}");
                Application.Current.Shutdown();
            }

            try
            {
                config = JsonConvert.DeserializeObject
                    <BotConfig>(rawJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{JSON_FILE_PATH} deserialization error: " + ex.Message);
                Application.Current.Shutdown();
            }
        }

        public static int ShortIntervalMs { get{ return config.shortIntervalMs; } }
        public static int LongIntervalMs { get{ return config.longIntervalMs; } }

        public static int SearchBarX { get { return config.searchBarX; } }
        public static int SearchBarY { get { return config.searchBarY; } }

        public static int MessageBoxX { get { return config.messageBoxX; } }
        public static int MessageBoxY { get { return config.messageBoxY; } }
    }
}