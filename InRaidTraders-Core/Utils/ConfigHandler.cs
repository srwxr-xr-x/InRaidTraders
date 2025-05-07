using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace InRaidTraders.Utils;

public class Config
{
    public string traderID { get; set; }
    public string traderName { get; set; }
    public int spawnChance { get; set; } = 100;
    public bool availableEverywhere { get; set; } = false;
    public bool hasServices { get; set; } = false;
    public KeyCode keybind { get; set; } = KeyCode.None;
    public string location { get; set; } = "0,0,0";
    public string rotation { get; set; } = "0,0,0";
    public string scale { get; set; } = "1,1,1";
    public string[] map { get; set; } = null;
    public bool DEBUG { get; set; } = false;
    
}

public static class ConfigHandler
{
    public static void LoadConfig()
    {
        foreach (String file in Directory.GetFiles(Globals.CONFIG_PATH))
        {
            using StreamReader reader = new(file);
            string json = reader.ReadToEnd();
            Config config = JsonConvert.DeserializeObject<Config>(json);
            if (config.traderID == null)
            {
                throw new ArgumentNullException(nameof(config.traderID));
            }
            if (config.traderName == null)
            {
                throw new ArgumentNullException(nameof(config.traderName));
            }
            if (config.map == null && !config.availableEverywhere)
            {
                throw new ArgumentNullException(nameof(config.map));
            }
            Globals.ConfigList.Add(config);
        }
    }
}