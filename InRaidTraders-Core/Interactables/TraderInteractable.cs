using EFT;
using EFT.Interactive;
using EFT.UI;
using EFT.UI.Screens;

namespace InRaidTraders.Interactables;

public class TraderInteractable : InteractableObject
{
    public void OnTalk()
    {
        // Get GUID from transform root
        new TraderDialogScreen.BTRDialogClass(GamePlayerOwner.MyPlayer.Profile,name.Replace(Globals.INTERACTIVE_UUID + " -- ", ""), GamePlayerOwner.MyPlayer.AbstractQuestControllerClass, GamePlayerOwner.MyPlayer.InventoryController, null).ShowScreen(EScreenState.Queued);
    }

    public ActionsReturnClass GetActions()
    {
        ActionsReturnClass actionsReturnClass = new ActionsReturnClass();
        actionsReturnClass.Actions.Add(new ActionsTypesClass
        {
            Action = OnTalk,
            Name = "Talk",
            Disabled = false,
        });
        
        return actionsReturnClass;
    }
}