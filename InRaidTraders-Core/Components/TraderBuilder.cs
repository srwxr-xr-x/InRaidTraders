using EFT.Interactive;
using UnityEngine;

namespace InRaidTraders.Components;

public class TraderBuilder
{
    
    private static string _traderID;
    private static Vector3 _position;
    private static Vector3 _scale;
    private static bool _debug;
    
    public static GameObject Build(string trader, Vector3 position, Vector3 scale, bool debug)
    {
        _traderID = trader;
        _position = position;
        _scale = scale;
        _debug = debug;

        if (_debug)
        {
            Plugin.LogSource.LogDebug("InteractableBuilder<" + _traderID + "> created");  
            
        }
        return CreateGameObject();
    }

    private static GameObject CreateGameObject()
    { 
        GameObject interactableObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        interactableObject.name = Globals.INTERACTIVE_UUID + " -- " + _traderID;
        interactableObject.transform.position = _position;
        interactableObject.transform.localScale = _scale;
        interactableObject.AddComponent<TraderInteractable>();
        interactableObject.GetComponent<BoxCollider>().enabled = false;
        interactableObject.GetComponent<MeshRenderer>().enabled = _debug;
        interactableObject.SetActive(true);
       
        return interactableObject;
    }
    
}