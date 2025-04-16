using System.Linq;
using System.Reflection;
using Comfort.Common;
using EFT;
using InRaidTraders.Components;
using SPT.Reflection.Patching;
using UnityEngine;
using Random = System.Random;

namespace InRaidTraders.Patches;

public class GameWorldStartPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod() {
        return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
    }

    [PatchPrefix]
    public static void PatchPrefix()
    {
        string playerLocation = Singleton<GameWorld>.Instance.MainPlayer.Location;
        int seedSpawnChance = new Random().Next(0, 100);
        
        if (seedSpawnChance <= Plugin.fenceSpawnChance.Value)
        {
            TraderBuilder.Build(Globals.FENCE_ID, Singleton<GameWorld>.Instance.MainPlayer.Transform.position, 
                new Vector3(1f, 1f, 1f), true);
        }
        
        if (playerLocation.Equals(Globals.WOODS))
        {
            if (seedSpawnChance <= Plugin.praporSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.PRAPOR_ID, new Vector3(-655.235f, 8.702f, 179.3f), 
                    new Vector3(1f, 1f, 1f), true);
            }
            if (seedSpawnChance <= Plugin.jaegerSpawnChance.Value)
            {
                int campsiteLocationRandomness = new Random().Next(0, 100);
                if (campsiteLocationRandomness <= 0.33f)
                {
                    // Campsite near Encrypted Message
                    TraderBuilder.Build(Globals.JAEGER_ID, new Vector3(-255.45f, 8.75f, 14.5f),
                        new Vector3(1f, 1.5f, 1f), true);
                } else if (campsiteLocationRandomness >= 0.33f && campsiteLocationRandomness <= 0.66f)
                {
                    // Gravesite with 'Protect the Sky' mission item
                    TraderBuilder.Build(Globals.JAEGER_ID, new Vector3(-55.02f, 9.35f, -500.9f),
                        new Vector3(1f, 1f, 1f), true);
                } else if (campsiteLocationRandomness >= 0.66f)
                {
                    // Behind large rock at the Signal Zone
                    
                    TraderBuilder.Build(Globals.JAEGER_ID, new Vector3(355.15f, 0f, -87f),
                        new Vector3(1f, 1f, 1f), true);
                }
            }
        }
        if (playerLocation.Equals(Globals.STREETS))
        {
            if (seedSpawnChance <= Plugin.therapistSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.THERAPIST_ID, Singleton<GameWorld>.Instance.MainPlayer.Transform.position,
                    new Vector3(1, 1, 1), true);
            }
        }
        if (playerLocation.Equals(Globals.SNORELINE))
        {
            if (seedSpawnChance <= Plugin.skierSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.SKIER_ID, Singleton<GameWorld>.Instance.MainPlayer.Transform.position,
                    new Vector3(1, 1, 1), true);
            }
        }
        if (playerLocation.Equals(Globals.SNORELINE))
        {
            if (seedSpawnChance <= Plugin.peacekeeperSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.PEACEKEEPER_ID, Singleton<GameWorld>.Instance.MainPlayer.Transform.position,
                    new Vector3(1, 1, 1), true);
            }
        }
        if (playerLocation.Equals(Globals.INTERCHANGE))
        {
            InterchangePatches();
            if (seedSpawnChance <= Plugin.ragmanSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.RAGMAN_ID, new Vector3(261.4f, 22f, -5f),
                    new Vector3(1f, 1.5f, 1f), true);
            }
        }
        if (playerLocation.Equals(Globals.FACTORY_NIGHT))
        {
            if (seedSpawnChance <= (Plugin.mechanicSpawnChance.Value / 8.5))
            {
                TraderBuilder.Build(Globals.MECHANIC_ID, Singleton<GameWorld>.Instance.MainPlayer.Transform.position,
                    new Vector3(1, 1, 1), true);
            }
        }
        if (playerLocation.Equals(Globals.FACTORY_DAY))
        {
            if (seedSpawnChance <= Plugin.mechanicSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.MECHANIC_ID, Singleton<GameWorld>.Instance.MainPlayer.Transform.position,
                    new Vector3(1, 1, 1), true);
            }
        }
        

        
        // Find and enable all the Interactive objects after Player.create<T>, since it overrides it.
        foreach (GameObject interact in Resources.FindObjectsOfTypeAll<GameObject>()
                     .Where(obj => obj.name.Contains(Globals.INTERACTIVE_UUID)))
        {
            interact.layer = LayerMask.NameToLayer("Interactive");
            interact.GetComponent<BoxCollider>().enabled = true;
        }
        
    }

    private static void InterchangePatches()
    {
        GameObject ragmanZone = GameObject.Find("/Zone_3-C");
        GameObject shoppingMall = GameObject.Find("/shopping_mall_shops_C19_details/");
        Transform tent = ragmanZone.transform.Find("inflatable_tent_update");
        Transform tentDoor = tent.Find("inflatable_tent_door_1");
        GameObject ragmanBed = GameObject.Instantiate(
            ragmanZone.transform.Find("bed_metal (6)").gameObject, 
            new Vector3(259.35f, 21.35f, -1.6f), 
            Quaternion.Euler(270, 90, 0));
        GameObject ragmanClothes = GameObject.Instantiate(
            shoppingMall.transform.Find("mall_WallCloth_C_Set1").gameObject, 
            new Vector3(261.72f, 21.35f, 0.8f), 
            Quaternion.Euler(270, 90, 0));
        // Raise tent so floor isn't pavement
        tent.localPosition = new Vector3(62.84f, 0.05f, -6.07f);
        
        // Rotate door into place (closed)
        tentDoor.rotation = Quaternion.Euler(0, 0, 0);
        tentDoor.localPosition = new Vector3(-4.50f, 0.51f, 0.92f);
        
        // Disable flipped table
        ragmanZone.transform.Find("Metal_Table (16)").gameObject.SetActive(false);
        // Disable cardboard trash
        ragmanZone.transform.Find("cardboard_set1 (19)").gameObject.SetActive(false);
        // Disable destroyed box
        ragmanZone.transform.Find("wood_box_crash2 (1)").gameObject.SetActive(false);
        // Disable misc. trash
        ragmanZone.transform.Find("Garbage_Parking_1 (52)").gameObject.SetActive(false);
        // Cardboard box pallet
        ragmanZone.transform.Find("pallet_cardboard_box_P (25)").localPosition = new Vector3(61.5f, 0.04f, -2.65f);
        // Pallet plywood bottom
        Transform plywoodBottom = ragmanZone.transform.Find("plywood_board_1 (22)");
        plywoodBottom.localPosition = new Vector3(61.475f, 0.08f, -2.65f);
        plywoodBottom.rotation = Quaternion.Euler(0, 0, 0);
        plywoodBottom.localScale = new Vector3(0.72f, 1f, 1f);
        // Table plywood
        ragmanZone.transform.Find("plywood_board_1 (23)").localPosition = new Vector3(64.4f, 0.08f, -7.9f);

        ragmanBed.transform.localScale = new Vector3(1.2f, 1.3f, 1.2f);

        ragmanClothes.transform.Find("mall_WallCloth_C_Set1_LOD0").GetComponent<MeshRenderer>().enabled = true;
        ragmanClothes.transform.Find("mall_WallCloth_C_Set1_Shadow_LOD0").GetComponent<MeshRenderer>().enabled = true;

    }
}