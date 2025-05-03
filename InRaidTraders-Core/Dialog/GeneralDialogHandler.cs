using System;
using EFT;
using EFT.InventoryLogic;
using EFT.Trading;

namespace InRaidTraders.Dialog;

public class GeneralDialogHandler(
    Profile profile,
    string traderId,
    AbstractQuestControllerClass quests,
    InventoryController inventoryController,
    GInterface238 animationController = null)
    : GClass2336(profile, traderId, quests, inventoryController, animationController)
{
    public override void SetUp()
    {
        ETraderDialogType dialogType = ETraderDialogType.Main;
        Enum dialogAction = GClass3353.EDialogState.CommonGreetigs;
        GInterface238 animationController = ginterface238_0;
        method_4(new DialogOptionDataStruct(dialogType, dialogAction, animationController?.InitialDescriptionKey));
    }

    public override GClass2363 CreateDialogList(DialogOptionDataStruct source)
    {
        ETraderDialogType dialogType = source.DialogType;
        if (dialogType == ETraderDialogType.Quit)
        {
            return new GClass2364(source);
        }
        switch (dialogType)
        {
            case ETraderDialogType.SubServices:
                return new GClass2369(Trader, profile_0, source, inventoryController_0);
            case ETraderDialogType.ServiceDeal:
                return new GClass2373(Trader, inventoryController_0, abstractQuestControllerClass, source);
            case ETraderDialogType.Services:
                return new GClass2366(Trader, profile_0, inventoryController_0, abstractQuestControllerClass, source);
            case ETraderDialogType.NewQuests:
                return new GClass2381(Trader, abstractQuestControllerClass, inventoryController_0, source);
            case ETraderDialogType.QuestList:
                return new GClass2382(Trader, abstractQuestControllerClass, inventoryController_0, this, source);
            case ETraderDialogType.Main:
                return new Dialog(Trader, abstractQuestControllerClass, inventoryController_0, this, source);
            case ETraderDialogType.None:
                return null;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}