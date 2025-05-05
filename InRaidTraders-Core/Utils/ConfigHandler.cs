using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace InRaidTraders.Utils;

public class Config
{
    public string traderID;
    public string traderName;
    public int spawnChance;
    public bool availableEverywhere;
    public bool hasServices;
    public KeyCode keybind;
    public string location;
    public string rotation;
    public string scale;
    public string map;
    public bool DEBUG;
}

public class ConfigHandler
{
    public static void LoadConfig()
    {
        foreach (String file in Directory.GetFiles(Globals.CONFIG_PATH))
        {
            using StreamReader reader = new(file);
            string json = reader.ReadToEnd();
            List<Config> config = JsonConvert.DeserializeObject<List<Config>>(json);
            Globals.ConfigList.Add(config);
        }
    }
}