using System.Reflection;
using EFT;
using EFT.InventoryLogic;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace InRaidTraders;

public class TraderDialogScreenPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(TraderDialogScreen), nameof(TraderDialogScreen.method_4));
    }

    [PatchPrefix]
    public static bool PatchPrefix(TraderDialogScreen __instance, 
                                                ref string ___string_0, 
                                                ref GClass2336 ___gclass2336_0, 
                                                ref Profile ___profile_0, 
                                                ref AbstractQuestControllerClass ___abstractQuestControllerClass, 
                                                ref InventoryController ___inventoryController_0, 
                                                ref GInterface238 ___ginterface238_0, ref AddViewListClass ___UI, ref TraderDialogWindow ____dialogWindow)
    {
        // This function contains (mostly) decompiler artifacts, this outlines what means what:
        // ___string_0 = trader mongo ID, for lookup with services and such
        // ___gclass2336_0 = dialog controller for traders
        // ___ginterface238_0 = animation controller
        // GClass2337 = BTR Dialog Handler
        // GClass2338 = Lightkeeper Dialog Handler
        
        if (___string_0 == Globals.PRAPOR_ID)
        {            
            ___gclass2336_0 = new GClass2338(___profile_0, Profile.TraderInfo.LIGHT_KEEPER_TRADER_ID, ___abstractQuestControllerClass, ___inventoryController_0, ___ginterface238_0);
            
            ___gclass2336_0.OnActionFinished += __instance.method_5;
            ___UI.AddDisposable(__instance.method_7);
            ___UI.AddDisposable(___gclass2336_0);
            ____dialogWindow.Show(___gclass2336_0);

            return false;
        }
        return true;
    }
}