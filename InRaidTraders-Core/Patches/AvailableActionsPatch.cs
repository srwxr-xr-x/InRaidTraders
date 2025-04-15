using System.Reflection;
using EFT;
using HarmonyLib;
using InRaidTraders.Components;
using SPT.Reflection.Patching;

namespace InRaidTraders.Patches;

public class AvailableActionsPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.FirstMethod(typeof(GetActionsClass), method => method.Name == nameof(GetActionsClass.GetAvailableActions) && method.GetParameters()[0].Name == "owner");
    }
    [PatchPrefix]
    static bool PatchPrefix(GamePlayerOwner owner, object interactive, ref ActionsReturnClass __result)
    {
        // Add the interactions to the list. 
        if (interactive is TraderInteractable)
        {
            TraderInteractable trader = interactive as TraderInteractable;
            ActionsReturnClass newResult = trader.GetActions();
            __result = newResult;
            return false;
        }
        
        return true;
    }
}