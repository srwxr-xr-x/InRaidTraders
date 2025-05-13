using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Comfort.Common;
using EFT;
using EFT.UI;
using EFT.UI.Screens;
using InRaidTraders.Patches;
using InRaidTraders.Utils;
using UnityEngine;
using static InRaidTraders.AssemblyInfoClass;

namespace InRaidTraders;

[BepInPlugin(InRaidTradersGUID,  InRaidTradersName, InRaidTradersVersion)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource LogSource;

    private const string SpawnChanceSectionName = "Trader Spawn Chances";
    internal static ConfigEntry<float> PraporSpawnChance;
    internal static ConfigEntry<float> TherapistSpawnChance;
    internal static ConfigEntry<float> FenceSpawnChance;
    internal static ConfigEntry<float> SkierSpawnChance;
    internal static ConfigEntry<float> PeacekeeperSpawnChance;
    internal static ConfigEntry<float> MechanicSpawnChance;
    internal static ConfigEntry<float> RagmanSpawnChance;
    internal static ConfigEntry<float> JaegerSpawnChance;
    private const string TraderSettingsSectionName = "Trader Settings";
    internal static ConfigEntry<bool> TradersOutOfRaid;

    private void Awake()
    {
        LogSource = Logger;
        LogSource.LogInfo("InRaidTraders loaded!");
        
        InitConfiguration();
        EnablePatches();

    }

    private void Update()
    {
        foreach (Config configItem in Globals.ConfigList)
        { 
            if (Input.GetKeyDown(configItem.keybind) && configItem.availableEverywhere && GamePlayerOwner.MyPlayer.Profile.TradersInfo[configItem.traderID].Unlocked) 
            {
                new TraderDialogScreen.BTRDialogClass(GamePlayerOwner.MyPlayer.Profile, configItem.traderID,
                    GamePlayerOwner.MyPlayer.AbstractQuestControllerClass,
                    GamePlayerOwner.MyPlayer.InventoryController, null).ShowScreen(EScreenState.Queued); }
        }

    }

    private void InitConfiguration()
    {
        PraporSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Prapor Spawn Chance", 
            85f,
            "The chance that Prapor will be in the raid");
        TherapistSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Therapist Spawn Chance", 
            90f,
            "The chance that Therapist will be in the raid");
        FenceSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Fence Spawn Chance", 
            25f,
            "The chance that Fence will be in the raid");
        SkierSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Skier Spawn Chance", 
            85f,
            "The chance that Skier will be in the raid");
        PeacekeeperSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Peacekeeper Spawn Chance", 
            85f,
            "The chance that Peacekeeper will be in the raid");
        MechanicSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Mechanic Spawn Chance", 
            85f,
            "The chance that Mechanic will be in the raid at night");
        RagmanSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Ragman Spawn Chance", 
            65f,
            "The chance that Ragman will be in the raid");
        JaegerSpawnChance = Config.Bind(
            SpawnChanceSectionName, 
            "Jaeger Spawn Chance", 
            66f,
            "The chance that Jaeger will be in the raid");
        TradersOutOfRaid = Config.Bind(
            TraderSettingsSectionName,
            "Enable Traders out of raid?",
            false,
            "Enable the default traders main menu along with traders in raid");
        ConfigHandler.LoadConfig();
    }
    private void EnablePatches()
    {
        new TraderDialogScreenPatch().Enable();
        new AvailableActionsPatch().Enable();
        new ShowPatch().Enable(); 
        new GameWorldStartPatch().Enable();
        new Method5Patch().Enable();
        new TraderCardPatch().Enable();
        new MenuScreenPatch().Enable();
    }
}
