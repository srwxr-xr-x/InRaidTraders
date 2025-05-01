using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Comfort.Common;
using EFT;
using EFT.UI;
using InRaidTraders.Helpers;
using InRaidTraders.Patches;
using UnityEngine;
using static InRaidTraders.AssemblyInfoClass;

namespace InRaidTraders;

[BepInPlugin(InRaidTradersGUID,  InRaidTradersName, InRaidTradersVersion)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource LogSource;

    private const string SPAWN_CHANCE_SECTION_NAME = "Trader Spawn Chances";
    internal static ConfigEntry<float> PraporSpawnChance;
    internal static ConfigEntry<float> TherapistSpawnChance;
    internal static ConfigEntry<float> FenceSpawnChance;
    internal static ConfigEntry<float> SkierSpawnChance;
    internal static ConfigEntry<float> PeacekeeperSpawnChance;
    internal static ConfigEntry<float> MechanicSpawnChance;
    internal static ConfigEntry<float> RagmanSpawnChance;
    internal static ConfigEntry<float> JaegerSpawnChance;

    private void Awake()
    {
        LogSource = Logger;
        LogSource.LogInfo("InRaidTraders loaded!");
        
        InitConfiguration();
        EnablePatches();
        AssetHelper.LoadBundles();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && Singleton<GameWorld>.Instantiated)
        {
            TraderClass[] tradeableArray = Singleton<ClientApplication<ISession>>.Instance.Session.Traders.Where(MainMenuControllerClass.Class1394.class1394_0.method_4).ToArray();
            var gClass3599 = new TraderScreensGroup.GClass3599(tradeableArray.First(), 
                tradeableArray, 
                Singleton<GameWorld>.Instance.MainPlayer.Profile, 
                Singleton<GameWorld>.Instance.MainPlayer.InventoryController, 
                Singleton<GameWorld>.Instance.MainPlayer.HealthController, 
                Singleton<GameWorld>.Instance.MainPlayer.AbstractQuestControllerClass, 
                Singleton<GameWorld>.Instance.MainPlayer.AbstractAchievementControllerClass,
                Singleton<ClientApplication<ISession>>.Instance.Session);
            MonoBehaviourSingleton<MenuUI>.Instance.TraderScreensGroup.method_2(gClass3599);
            MonoBehaviourSingleton<MenuUI>.Instance.TraderScreensGroup.Awake();
            MonoBehaviourSingleton<MenuUI>.Instance.TraderScreensGroup.Show(gClass3599);
        }
    }

    private void InitConfiguration()
    {
        PraporSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Prapor Spawn Chance", 
            85f,
            "The chance that Prapor will be in the raid");
        TherapistSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Therapist Spawn Chance", 
            90f,
            "The chance that Therapist will be in the raid");
        FenceSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Fence Spawn Chance", 
            25f,
            "The chance that Fence will be in the raid");
        SkierSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Skier Spawn Chance", 
            85f,
            "The chance that Skier will be in the raid");
        PeacekeeperSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Peacekeeper Spawn Chance", 
            85f,
            "The chance that Peacekeeper will be in the raid");
        MechanicSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Mechanic Spawn Chance", 
            85f,
            "The chance that Mechanic will be in the raid at night");
        RagmanSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Ragman Spawn Chance", 
            65f,
            "The chance that Ragman will be in the raid");
        JaegerSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Jaeger Spawn Chance", 
            66f,
            "The chance that Jaeger will be in the raid");
    }
    private void EnablePatches()
    {
        new TraderDialogScreenPatch().Enable();
        new AvailableActionsPatch().Enable();
        new GameWorldStartPatch().Enable();
        new TraderScreensGroupPatch().Enable(); 
        new TradingPlayerPanelPatch().Enable();
    }
    
}
