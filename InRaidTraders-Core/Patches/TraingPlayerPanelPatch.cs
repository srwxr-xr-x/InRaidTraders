using System.Reflection;
using EFT;
using EFT.UI;
using HarmonyLib;
using PlayerIcons;
using SPT.Reflection.Patching;
using UnityEngine.UI;

namespace InRaidTraders.Patches;

public class TradingPlayerPanelPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(TradingPlayerPanel), nameof(TradingPlayerPanel.Set));
    }

    [PatchPrefix]
    public static bool Prefix(TradingPlayerPanel __instance, Profile profile, Profile.TraderInfo traderInfo, CustomTextMeshProUGUI ____nickname, PlayerIconImage ____playerIconImage, Image ____levelIcon, DisplayMoneyPanelTMPText ____moneyCountPanel )
    {
        __instance.ShowGameObject();
        PlayerLevelPanel.SetLevelIcon(____levelIcon, profile.Info.Level);
        ____nickname.text = profile.GetCorrectedNickname() + " (" + "You in trading".Localized(null) + ")";
        ____moneyCountPanel.Show(profile.Inventory.GetPlayerItems()); // Crashing here!
        ____playerIconImage.SetPresetIcon(profile);
        __instance.UpdateStats(traderInfo);
        return false;
    }
    
}