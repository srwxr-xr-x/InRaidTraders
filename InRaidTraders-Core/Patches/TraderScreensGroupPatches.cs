using System.Collections.Generic;
using System.Reflection;
using Comfort.Common;
using EFT;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using UnityEngine;

namespace InRaidTraders.Patches;

public class ShowPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AccessTools.Method(typeof(TraderScreensGroup), nameof(TraderScreensGroup.Show));
    }

    [PatchPrefix]
    public static bool Prefix(TraderScreensGroup __instance, ref Tab ____tasksTab, ref Tab ____servicesTab)
    {
        ____tasksTab.gameObject.SetActive(false);
        ____servicesTab.gameObject.RectTransform().anchoredPosition = new Vector2(130, 2);
        return true;
    }
}

