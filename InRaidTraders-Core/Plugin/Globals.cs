using EFT;

namespace InRaidTraders;

public static class Globals
{
    // Mongo ID's of every trader, constants to use across the entire mod. Some values are already set and used for various purposes in EFT, so we just fetch those.
    // This may not be the best solution
    public const string PRAPOR_ID = "67fdd0bbc2587a7e91025829";
    public const string THERAPIST_ID = "67fdd124c2587a7e91025832";
    public const string FENCE_ID = Profile.TraderInfo.FENCE_ID;
    public const string SKIER_ID = "67fdd1abeb2a2a46758fea5d";
    public const string PEACEKEEPER_ID = "67fdd1ba227dbeff8c9ddbae";
    public const string MECHANIC_ID = "67fdd1c72debc72198ba1417";
    public const string RAGMAN_ID = Profile.TraderInfo.RAGMAN_TRADER_ID;
    public const string JAEGER_ID = "67fdd21b64ec335ce16fbe26";
    public const string REF_ID = Profile.TraderInfo.ARENA_MANAGER_TRADER_ID;
    public const string LIGHT_KEEPER_ID = Profile.TraderInfo.LIGHT_KEEPER_TRADER_ID;
    public const string BTR_DRIVER_ID = Profile.TraderInfo.BTR_TRADER_ID;
}