using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using InRaidTraders.Patches;
using static InRaidTraders.AssemblyInfoClass;

namespace InRaidTraders;

[BepInPlugin(InRaidTradersGUID,  InRaidTradersName, InRaidTradersVersion)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource LogSource;

    private const string SPAWN_CHANCE_SECTION_NAME = "Trader Spawn Chances";
    internal static ConfigEntry<float> praporSpawnChance;
    internal static ConfigEntry<float> therapistSpawnChance;
    internal static ConfigEntry<float> fenceSpawnChance;
    internal static ConfigEntry<float> skierSpawnChance;
    internal static ConfigEntry<float> peacekeeperSpawnChance;
    internal static ConfigEntry<float> mechanicSpawnChance;
    internal static ConfigEntry<float> ragmanSpawnChance;
    internal static ConfigEntry<float> jaegerSpawnChance;
    internal static ConfigEntry<float> refSpawnChance;

    private void Awake()
    {
        LogSource = Logger;
        LogSource.LogInfo("InRaidTraders loaded!");

        InitConfiguration();
        EnablePatches();
    }

    private void InitConfiguration()
    {
        praporSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Prapor Spawn Chance", 
            85f,
            "The chance that Prapor will be in the raid");
        therapistSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Therapist Spawn Chance", 
            90f,
            "The chance that Therapist will be in the raid");
        fenceSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Fence Spawn Chance", 
            25f,
            "The chance that Fence will be in the raid");
        skierSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Skier Spawn Chance", 
            85f,
            "The chance that Skier will be in the raid");
        peacekeeperSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Peacekeeper Spawn Chance", 
            85f,
            "The chance that Peacekeeper will be in the raid");
        mechanicSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Mechanic Spawn Chance", 
            85f,
            "The chance that Mechanic will be in the raid at night");
        ragmanSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Ragman Spawn Chance", 
            65f,
            "The chance that Ragman will be in the raid");
        jaegerSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Jaeger Spawn Chance", 
            66f,
            "The chance that Jaeger will be in the raid");
        refSpawnChance = Config.Bind(
            SPAWN_CHANCE_SECTION_NAME, 
            "Ref Spawn Chance", 
            100f,
            "The chance that Ref will be in the raid");


    }
    private void EnablePatches()
    {
        new TraderDialogScreenPatch().Enable();
        new AvailableActionsPatch().Enable();
        new GameWorldStartPatch().Enable();
        new TraderScreensGroupPatch().Enable();        
    }
}
