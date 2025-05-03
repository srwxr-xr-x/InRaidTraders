using System.Linq;
using System.Reflection;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.UI;
using InRaidTraders.Builders;
using SPT.Reflection.Patching;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InRaidTraders.Patches;

public class GameWorldStartPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod() {
        return typeof(GameWorld).GetMethod(nameof(GameWorld.OnGameStarted));
    }

    [PatchPrefix]
    public static void PatchPrefix()
    {
        Player player = Singleton<GameWorld>.Instance.MainPlayer;
        int seedSpawnChance = Random.Range(0, 100);
        
        if (player.Location.Equals(Globals.WOODS))
        {
            if (seedSpawnChance <= Plugin.PraporSpawnChance.Value)
            {
                GameObject door = GameObject.Find("SBG_Woods_Combined/SOO_LOD0/DOORS/Outside_Door_Wood_09_L_210-100/Outside_Door_Wood_09_L_210-100_door");
                door.GetComponent<Door>().DoorState -= EDoorState.Locked;
                door.GetComponent<Door>().DoorState = EDoorState.Shut;   
                
                TraderBuilder.Build(Globals.PRAPOR_ID, new Vector3(-655.235f, 8.702f, 179.3f), 
                    new Vector3(0, 0, 0),new Vector3(1f, 1f, 1f), true);
            }
            if (seedSpawnChance <= Plugin.JaegerSpawnChance.Value && player.Profile.TradersInfo[Globals.JAEGER_ID].Unlocked)
            {
                int campsiteLocationRandomness = Random.Range(0, 100);
                if (campsiteLocationRandomness <= 0.33f)
                {
                    // Campsite near Encrypted Message
                    TraderBuilder.Build(Globals.JAEGER_ID, new Vector3(-255.45f, 8.75f, 14.5f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.5f, 1f), true);
                } else if (campsiteLocationRandomness >= 0.33f && campsiteLocationRandomness <= 0.66f)
                {
                    // Gravesite with 'Protect the Sky' mission item
                    TraderBuilder.Build(Globals.JAEGER_ID, new Vector3(-55.02f, 9.35f, -500.9f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.5f, 1f), true);
                } else if (campsiteLocationRandomness >= 0.66f)
                {
                    // Behind large rock at the Signal Zone
                    TraderBuilder.Build(Globals.JAEGER_ID, new Vector3(355.15f, 0f, -87f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.5f, 1f), true);
                }
            }
        }
        if (player.Location.Equals(Globals.STREETS))
        {
            StreetsPatches();
            if (seedSpawnChance <= Plugin.TherapistSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.THERAPIST_ID, new Vector3(-180.89f, 3.53f, 255.56f),
                    new Vector3(0, 0, 0),new Vector3(0.5f, 0.5f, 0.5f), true);
            }
            
            if (seedSpawnChance <= Plugin.FenceSpawnChance.Value)
            {
                int fenceLocationRandomness = Random.Range(0, 100);
                if (fenceLocationRandomness <= 0.33f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(-71.7f, 4f, 265.3f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.5f, 1f), true);
                } else if (fenceLocationRandomness >= 0.33f && fenceLocationRandomness <= 0.66f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(102.9f, -0.5f, 87.5f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.5f, 1f), true);
                } else if (fenceLocationRandomness >= 0.66f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(-90.8f, -1.45f, 178.12f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.5f, 1f), true);
                }
            }
        }
        if (player.Location.Equals(Globals.SNORELINE))
        {
            SnorelinePatches();
            if (seedSpawnChance <= Plugin.SkierSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.SKIER_ID, new Vector3(-911.8f, -48.4f, 262.6f),
                    new Vector3(0, 0, 0),new Vector3(0.8f, 1.5f, 0.8f), true);
            }
            if (seedSpawnChance <= Plugin.PeacekeeperSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.PEACEKEEPER_ID, Singleton<GameWorld>.Instance.MainPlayer.Transform.position,
                    new Vector3(0, 0, 0),new Vector3(1, 1, 1), true);
            }
            if (seedSpawnChance <= Plugin.FenceSpawnChance.Value)
            {
                int fenceLocationRandomness = Random.Range(0, 100);
                if (fenceLocationRandomness <= 0.33f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(280.1f, -58.5f, 295.2f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
                } else if (fenceLocationRandomness >= 0.33f && fenceLocationRandomness <= 0.66f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(-233.8f, -40f, 165.7f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
                } else if (fenceLocationRandomness >= 0.66f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(-243.6f, -1.6f, -139f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
                }
            }

        }
        if (player.Location.Equals(Globals.INTERCHANGE))
        {
            InterchangePatches();
            if (seedSpawnChance <= Plugin.RagmanSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.RAGMAN_ID, new Vector3(261.4f, 22f, -4.8f),
                    new Vector3(0, 0, 0),new Vector3(1f, 1.5f, 1f), true);
            }
            if (seedSpawnChance <= Plugin.FenceSpawnChance.Value)
            {
                int fenceLocationRandomness = Random.Range(0, 100);
                if (fenceLocationRandomness <= 0.33f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(314.2f, 19.7f, -366.5f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
                } else if (fenceLocationRandomness >= 0.33f && fenceLocationRandomness <= 0.66f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(11.5f, 22.2f, -167.25f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
                } else if (fenceLocationRandomness >= 0.66f)
                {
                    TraderBuilder.Build(Globals.FENCE_ID, new Vector3(-19.8f, 22.4f, 171.3f),
                        new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
                }
            }
        }
        if (player.Location.Equals(Globals.FACTORY_NIGHT))
        {
            FactoryPatches();
            if (seedSpawnChance <= (Plugin.MechanicSpawnChance.Value))
            {
                TraderBuilder.Build(Globals.MECHANIC_ID, new Vector3(-15.21f, -1.23f, 43.4f),
                    new Vector3(0, 0, 0),new Vector3(0.5f, 0.5f, 0.5f), true);
            }
            if (seedSpawnChance <= Plugin.FenceSpawnChance.Value)
            {
                TraderBuilder.Build(Globals.FENCE_ID, new Vector3(5.1f, -0.77f, 65.4f),
                    new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
            }
        }
        if (player.Location.Equals(Globals.FACTORY_DAY))
        {
            FactoryPatches();
            if (seedSpawnChance <= (Plugin.MechanicSpawnChance.Value / 8.5))
            {
                TraderBuilder.Build(Globals.MECHANIC_ID, new Vector3(-15.21f, -1.23f, 43.4f),
                    new Vector3(0, 0, 0),new Vector3(0.5f, 0.5f, 0.5f), true);
            }
            if (seedSpawnChance <= Plugin.FenceSpawnChance.Value)
            {

                TraderBuilder.Build(Globals.FENCE_ID, new Vector3(5.1f, -0.77f, 65.4f),
                    new Vector3(0, 0, 0),new Vector3(1f, 1.75f, 1f), true);
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
    [PatchPostfix]
    public static void PatchPostfix()
    {
        Singleton<GameWorld>.Instance.MainPlayer.Inventory.Stash = Globals.playerStash;
        Singleton<GameWorld>.Instance.MainPlayer.Inventory.QuestStashItems = Singleton<ItemFactoryClass>.Instance.CreateFakeStash();
        AssetsManagerSingletonClass.Manager.LoadScene(GClass2078.MenuUIScene, LoadSceneMode.Additive);
    }

    private static void UIPatches()
    {
        GameObject traderScreensGroup = MonoBehaviourSingleton<MenuUI>.Instance.TraderScreensGroup.gameObject;
        GameObject container = traderScreensGroup.transform.Find("TopPanel/Container/").gameObject;
        container.transform.Find("Right Person/Tile").localPosition = new Vector3(-490f, -85.5f, 0f);

        container.transform.Find("Right Person/Background Tile").gameObject.SetActive(false);
        container.transform.Find("Pattern").gameObject.SetActive(false);
        container.transform.Find("SeparatorBottom").gameObject.SetActive(false);
        container.transform.Find("SeparatorTop").gameObject.SetActive(false);
        traderScreensGroup.transform.Find("/Tab Bar/Tabs/Services").gameObject.SetActive(false);
        
        container.transform.Find("Right Person/Background").localPosition = new Vector3(-542f, -85.5f, 0f);
        container.transform.Find("Right Person/Background").localScale = new Vector3(1.175f, 1f, 1f);
    }
    private static void StreetsPatches()
    {
        GameObject streets = GameObject.Find("SBG_City_NE_02_TD_Klimova_indoor_2ST_NEW/OO/PROPS/F1_Switchboard/small/");
        GameObject panel = GameObject.Instantiate(
            streets.transform.Find("Old_Electrical_Cabinet_B").gameObject, 
            new Vector3(-180.89f, 3.43f, 255.56f), 
            Quaternion.Euler(270f, 297.45f, 0f));
        panel.transform.Find("Old_Electrical_Cabinet_B_LOD0").GetComponent<MeshRenderer>().enabled = true;
        panel.transform.Find("Old_Electrical_Cabinet_B_LOD1").GetComponent<MeshRenderer>().enabled = true;
        panel.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }
    private static void SnorelinePatches()
    {
        GameObject smugglerBase = GameObject.Find("SBG_Shoreline_East/OO/AGRO_TARK_ZONE/SmuglerBase_10/");
        
        GameObject chair = GameObject.Instantiate(
            smugglerBase.transform.Find("folding_chair").gameObject, 
            new Vector3(-911.2f, -49.23f, 259.76f), 
            Quaternion.Euler(270, 333, 0));
        chair.transform.Find("folding_Chair_LOD0").GetComponent<MeshRenderer>().enabled = true;
        chair.transform.Find("folding_Chair_LOD0").localScale = new Vector3(1.5f, 1.5f, 1.5f);
        
        GameObject chairDesk = GameObject.Instantiate(
            smugglerBase.transform.Find("folding_chair").gameObject, 
            new Vector3(-911.8f, -49.23f, 262.62f), 
            Quaternion.Euler(270, 26, 0));
        chairDesk.transform.Find("folding_Chair_LOD0").GetComponent<MeshRenderer>().enabled = true;
        chairDesk.transform.Find("folding_Chair_LOD0").localScale = new Vector3(1f, 1f, 1f);
        
        GameObject table = GameObject.Instantiate(
            smugglerBase.transform.Find("folding_table").gameObject, 
            new Vector3(-912.67f, -49.23f, 262.19f), 
            Quaternion.Euler(270, 246, 0));
        table.transform.Find("folding_Table_LOD0").GetComponent<MeshRenderer>().enabled = true;

        GameObject radio = GameObject.Instantiate(
            smugglerBase.transform.Find("M3RT_Radiostation").gameObject, 
            new Vector3(-912.4f, -48.4627f, 261.66f), 
            Quaternion.Euler(270f, 45f, 0f));
        radio.transform.Find("M3TR_LOD0").GetComponent<MeshRenderer>().enabled = true;

        GameObject radioHandset = GameObject.Instantiate(
            smugglerBase.transform.Find("M3TR_radiohandset_2").gameObject, 
            new Vector3(-912.347f, -48.4627f, 262.12f), 
            Quaternion.Euler(270f, 45f, 0f));
        radioHandset.transform.Find("M3TR_radiohandset_LOD0").GetComponent<MeshRenderer>().enabled = true;
        radioHandset.transform.Find("M3TR_radiohandset_LOD1").GetComponent<MeshRenderer>().enabled = true;

    }
    private static void FactoryPatches()
    {
        GameObject factoryDoor = GameObject.Find("/SBG_Factory_Rework_Basement/OO/");
        factoryDoor.transform.Find("Metal_barrel_04_closed_old_blue (5)").gameObject.SetActive(false);
        factoryDoor.transform.Find("metal_barrel_2_old_green_rezerv (2)").gameObject.SetActive(false);
        
        GameObject factoryPanel = GameObject.Instantiate(
            factoryDoor.transform.Find("basement_03 Group/Factory_basement_03_devices/Old_Electrical_Cabinet_B").gameObject, 
            new Vector3(-15.2f, -1.36f, 43.57f), 
            Quaternion.Euler(270, 0, 0));
        factoryPanel.transform.Find("Old_Electrical_Cabinet_B_LOD0").GetComponent<MeshRenderer>().enabled = true;
        factoryPanel.transform.Find("Old_Electrical_Cabinet_B_LOD1").GetComponent<MeshRenderer>().enabled = true;
    }
    private static void InterchangePatches()
    {
        GameObject zone3D = GameObject.Find("/Zone_3-D");
        GameObject ragmanZone = GameObject.Find("/Zone_3-C");
        GameObject shoppingMallC19 = GameObject.Find("/shopping_mall_shops_C19_details");
        GameObject shoppingMallD13 = GameObject.Find("/shopping_mall_shops_D13_details");
        GameObject shoppingMallC13 = GameObject.Find("/shopping_mall_shops_C13_details");
        Transform tent = ragmanZone.transform.Find("inflatable_tent_update");
        Transform tentDoor = tent.Find("inflatable_tent_door_1");
        GameObject ragmanBed = GameObject.Instantiate(
            ragmanZone.transform.Find("bed_metal (6)").gameObject, 
            new Vector3(259.25f, 21.35f, -1.6f), 
            Quaternion.Euler(270, 90, 0));
        GameObject ragmanClothes = GameObject.Instantiate(
            shoppingMallC19.transform.Find("mall_WallCloth_C_Set1").gameObject, 
            new Vector3(261.72f, 21.35f, 0.8f), 
            Quaternion.Euler(270, 90, 0));
        // Raise tent so floor isn't pavement
        tent.localPosition = new Vector3(62.84f, 0.05f, -6.07f);
        GameObject ragmanMannequin = GameObject.Instantiate(
            shoppingMallD13.transform.Find("Mannequin_var2_Men_update").gameObject, 
            new Vector3(262.24f, 21.35f, -4.1f), 
            Quaternion.Euler(270, 270, 0));
        GameObject ragmanMattress = GameObject.Instantiate(
            zone3D.transform.Find("matress4_update").gameObject, 
            new Vector3(259.07f, 21.825f, -1.65f), 
            Quaternion.Euler(270, 180, 0));
        GameObject ragmanPillow = GameObject.Instantiate(
            zone3D.transform.Find("cushion_1 (3)").gameObject, 
            new Vector3(259f, 22.1f, -1.27f), 
            Quaternion.Euler(37.85f, 113.75f, 9.25f));
        GameObject ragmanChair = GameObject.Instantiate(
            shoppingMallC13.transform.Find("armchair (6)").gameObject, 
            new Vector3(261.35f, 21.35f, -4.8f), 
            Quaternion.Euler(270f, 129f, 0f));
        
        // Rotate door into place (closed)
        tentDoor.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
        ragmanMattress.transform.localScale = new Vector3(1.3f, 1.1f, 1.2f);

        ragmanClothes.transform.Find("mall_WallCloth_C_Set1_LOD0").GetComponent<MeshRenderer>().enabled = true;
        ragmanMannequin.transform.Find("Mannequin_var2_Men_LOD0").GetComponent<MeshRenderer>().enabled = true;
        ragmanBed.transform.Find("bed_metall_LOD0").GetComponent<MeshRenderer>().enabled = true;
        ragmanMattress.transform.Find("matress4_LOD0").GetComponent<MeshRenderer>().enabled = true;
        ragmanPillow.transform.Find("cushion_1").GetComponent<MeshRenderer>().enabled = true;
        ragmanChair.transform.Find("armchair 1").GetComponent<MeshRenderer>().enabled = true;
    }
}