using System.Linq;
using System.Reflection;
using Comfort.Common;
using EFT;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using UnityEngine;

namespace InRaidTraders.Patches;

public class MenuScreenPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(MenuScreen), nameof(MenuScreen.method_3));
    }

    [PatchPostfix]
    public static void Postfix(MenuScreen __instance, DefaultUIButton ____tradeButton, DefaultUIButton ____hideoutButton, DefaultUIButton ____exitButton)
    {
        bool traderDisable = true;
        foreach (TraderClass trader in Singleton<ClientApplication<ISession>>.Instance.Session.Traders.Where(MainMenuControllerClass.Class1394.class1394_0.method_4).ToArray())
        {
            if (Utils.Utils.TraderIdToName(trader.Id) == "BAD_TRADER_ID")
            { 
                traderDisable = false;
            }
        }
        if (traderDisable)
        {
            MonoBehaviourSingleton<PreloaderUI>.Instance.MenuTaskBar.transform.Find("Tabs").Find("Merchants").gameObject.SetActive(false);
            ____tradeButton.transform.gameObject.SetActive(false);
            ____hideoutButton.transform.localPosition = new Vector3(0f, -272.5f, 0f);
            ____exitButton.transform.parent.localPosition = new Vector3(0f, -390f, 0f);
        }
    }
}
