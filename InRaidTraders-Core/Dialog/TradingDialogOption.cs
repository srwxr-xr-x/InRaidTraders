using EFT;
using EFT.InventoryLogic;
using EFT.Trading;

namespace InRaidTraders.Dialog;

public class TradingDialogOption : GClass2379
{
    public TradingDialogOption(Profile.TraderInfo trader, AbstractQuestControllerClass quests, InventoryController inventoryController, DialogOptionDataStruct source) : base(trader, quests, inventoryController, source)
    {
        this.BaseDescriptionKey = this.Source.DescriptionKey;
        string traderID = traderInfo_0.Id;
    }

    public override ETraderDialogType Type { get; }
    public override string BaseDescriptionKey { get; }
}