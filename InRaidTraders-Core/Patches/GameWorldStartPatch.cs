using System.Linq;
using System.Reflection;
using Comfort.Common;
using EFT;
using InRaidTraders.Components;
using SPT.Reflection.Patching;
using UnityEngine;

namespace InRaidTraders.Patches;

public class GameWorldStartPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod() {
        return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
    }

    [PatchPrefix]
    public static void PatchPrefix()
    {
        GameObject interactableObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        interactableObject.name = Globals.INTERACTIVE_UUID + " -- " + Globals.PRAPOR_ID;
        interactableObject.transform.position = Singleton<GameWorld>.Instance.MainPlayer.Transform.position;
        interactableObject.transform.localScale = new Vector3(1, 1, 1);
        interactableObject.AddComponent<TraderInteractable>();
        interactableObject.GetComponent<BoxCollider>().enabled = false;
        interactableObject.GetComponent<MeshRenderer>().enabled = true;
        interactableObject.SetActive(true);
        
        // Find and enable all the Interactive objects after Player.create<T>, since it overrides it.
        foreach (GameObject interact in Resources.FindObjectsOfTypeAll<GameObject>()
                     .Where(obj => obj.name.Contains(Globals.INTERACTIVE_UUID)))
        {
            interact.layer = LayerMask.NameToLayer("Interactive");
            interact.GetComponent<BoxCollider>().enabled = true;
        }
        
    }
}