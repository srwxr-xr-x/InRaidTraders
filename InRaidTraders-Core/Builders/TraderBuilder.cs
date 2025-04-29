using InRaidTraders.Interactables;
using UnityEngine;

namespace InRaidTraders.Builders;

public class TraderBuilder
{
    
    private static string _traderID;
    private static Vector3 _position;
    private static Vector3 _rotation;
    private static Vector3 _scale;
    private static bool _debug;
    
    public static GameObject Build(string trader, Vector3 position, Vector3 rotation, Vector3 scale, bool debug)
    {
        _traderID = trader;
        _position = position;
        _rotation = rotation;
        _scale = scale;
        _debug = debug;

        if (_debug)
        {
            Plugin.Plugin.LogSource.LogDebug("InteractableBuilder<" + _traderID + "> created");  
            
        }
        return CreateGameObject();
    }

    private static GameObject CreateGameObject()
    {
        GameObject interactableObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        interactableObject.name = Globals.INTERACTIVE_UUID + " -- " + _traderID;

        if (Globals.PRAPOR_ID == _traderID) // Testing with only the prapor bundle lolz
        {
            if (_traderID is not (Globals.MECHANIC_ID or Globals.THERAPIST_ID))
            {
                GameObject traderPrefab = Utils.Utils.MapIDToAssetBundle(_traderID)
                    .LoadAsset<GameObject>("assets/wtt/traders/" + Utils.Utils.TraderIdToName(_traderID).ToLower() +
                                           "_take3.prefab");
                GameObject traderObject = Object.Instantiate(traderPrefab, _position, Quaternion.Euler(_rotation));
                interactableObject.transform.parent = traderObject.transform;

            }
        }

        interactableObject.transform.localPosition = new Vector3(0, 1, 0);
        interactableObject.transform.localScale = _scale;
        interactableObject.AddComponent<TraderInteractable>();
        interactableObject.GetComponent<BoxCollider>().enabled = false;
        interactableObject.GetComponent<MeshRenderer>().enabled = _debug;
        interactableObject.SetActive(true);
       
        return interactableObject;
    }


}