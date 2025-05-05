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
        Globals.PlayerStash = __instance.InventoryController.Inventory.Stash;
        Globals.InventoryController = __instance.InventoryController;
    }
}
