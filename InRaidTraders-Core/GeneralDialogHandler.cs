using System;
using EFT;
using EFT.InventoryLogic;
using EFT.Trading;

namespace InRaidTraders;

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

    public override void ServiceUpdateHandler()
    {
        if (CurrentDialog == null)
        {
            return;
        }
        ETraderDialogType type = CurrentDialog.Type;
        if (type - ETraderDialogType.SubServices > 2)
        {
            if (type == ETraderDialogType.Main)
            {
                CurrentDialog = new GClass2380(Trader, abstractQuestControllerClass, inventoryController_0, this, CurrentDialog.Source);
            }
        }
        else
        {
            CurrentDialog = new GClass2366(Trader, profile_0, inventoryController_0, abstractQuestControllerClass, CurrentDialog.Source);
        }
    }

    // Token: 0x0600CFC2 RID: 53186 RVA: 0x005407EC File Offset: 0x0053E9EC
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
                return new GClass2372(Trader, inventoryController_0, abstractQuestControllerClass, source);
            case ETraderDialogType.Services:
                return new GClass2366(Trader, profile_0, inventoryController_0, abstractQuestControllerClass, source);
            case ETraderDialogType.NewQuests:
                return new GClass2381(Trader, abstractQuestControllerClass, inventoryController_0, source);
            case ETraderDialogType.QuestList:
                return new GClass2382(Trader, abstractQuestControllerClass, inventoryController_0, this, source);
            case ETraderDialogType.Main:
                return new GClass2380(Trader, abstractQuestControllerClass, inventoryController_0, this, source);
            case ETraderDialogType.None:
                return null;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}