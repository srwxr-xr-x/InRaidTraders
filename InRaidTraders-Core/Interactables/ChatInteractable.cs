using EFT.Interactive;

namespace InRaidTraders.Interactables;

public class ChatInteractable : InteractableObject
{
    public void OnChat()
    {
        CurrentScreenSingletonClass.Instance.OpenChatScreen();
    }

    public ActionsReturnClass GetActions()
    {
        ActionsReturnClass actionsReturnClass = new ActionsReturnClass();
        actionsReturnClass.Actions.Add(new ActionsTypesClass
        {
            Action = OnChat,
            Name = "Open Chat",
            Disabled = false,
        });
        
        return actionsReturnClass;
    }
}