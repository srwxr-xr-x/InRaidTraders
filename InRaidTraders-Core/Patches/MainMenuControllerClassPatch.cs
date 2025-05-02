using System.Reflection;
using EFT;
using EFT.UI;
using HarmonyLib;
using PlayerIcons;
using SPT.Reflection.Patching;

namespace InRaidTraders.Patches;

public class MainMenuControllerClassPatch: ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MainMenuControllerClass), nameof(MainMenuControllerClass.method_5));
    }

    [PatchPostfix]
    public static void Postfix(MainMenuControllerClass __instance)
    {
        Globals.playerStash = __instance.InventoryController.Inventory.Stash;
        Plugin.LogSource.LogWarning(Globals.playerStash);
    }
}