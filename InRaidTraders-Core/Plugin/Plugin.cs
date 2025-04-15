using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using EFT;
using EFT.UI;
using EFT.UI.Screens;
using InRaidTraders.Patches;
using UnityEngine;
using static InRaidTraders.AssemblyInfoClass;

namespace InRaidTraders;

[BepInPlugin(InRaidTradersGUID,  InRaidTradersName, InRaidTradersVersion)]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource LogSource;
    private void Awake()
    {
        LogSource = Logger;
        LogSource.LogInfo("InRaidTraders loaded!");
        
        new TraderDialogScreenPatch().Enable();
        new AvailableActionsPatch().Enable();
        new GameWorldStartPatch().Enable();
    }
        
    private void Update()
    {

        
    }
}
