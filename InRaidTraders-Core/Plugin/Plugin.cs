﻿using BepInEx;
using BepInEx.Logging;
using EFT;
using EFT.UI;
using EFT.UI.Screens;
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
    }
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            new TraderDialogScreen.BTRDialogClass(GamePlayerOwner.MyPlayer.Profile, "656f0f98d80a697f855d34b1", GamePlayerOwner.MyPlayer.AbstractQuestControllerClass, GamePlayerOwner.MyPlayer.InventoryController, null).ShowScreen(EScreenState.Queued);
        }
    }
}
