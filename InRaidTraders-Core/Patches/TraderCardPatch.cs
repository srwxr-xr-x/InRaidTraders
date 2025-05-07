using System.Reflection;
using Comfort.Common;
using EFT;
using SPT.Reflection.Patching;

namespace InRaidTraders.Patches;

public class TraderCardPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod() =>
        typeof(TraderCard).GetMethod("Show", BindingFlags.Instance | BindingFlags.Public);

    [PatchPostfix]
    protected static void Postfix(TraderCard __instance, ref Profile.TraderInfo trader)
    {
        if (Utils.Utils.TraderIdToName(trader.Id) != "BAD_TRADER_ID" && !Plugin.TradersOutOfRaid.Value)
        {
            __instance.HideGameObject();
        }
        if (Singleton<GameWorld>.Instantiated)
        {
            __instance.ShowGameObject();
        }
    }
}