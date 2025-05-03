using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace InRaidTraders.Patches;

public class Method5Patch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MainMenuControllerClass), nameof(MainMenuControllerClass.method_5));
    }

    [PatchPostfix]
    public static void Postfix(MainMenuControllerClass __instance)
    {
        Globals.playerStash = __instance.InventoryController.Inventory.Stash;
    }
}