namespace InRaidTraders;

public static class Utils
{
    public static string TraderIdToName(string traderId)
    {
        if (traderId == "54cb50c76803fa8b248b4571")
        {
            return "Prapor";
        }
        if (traderId == "54cb57776803fa99248b456e")
        {
            return "Therapist";
        }
        if (traderId == "579dc571d53a0658a154fbec")
        {
            return "Fence";
        }
        if (traderId == "58330581ace78e27b8b10cee")
        {
            return "Skier";
        }
        if (traderId == "5935c25fb3acc3127c3d8cd9")
        {
            return "Peacekeeper";
        }
        if (traderId == "5a7c2eca46aef81a7ca2145d")
        {
            return "Mechanic";
        }
        if (traderId == "5ac3b934156ae10c4430e83c")
        {
            return "Ragman";
        }
        if (traderId == "5c0647fdd443bc2504c2d371")
        {
            return "Jaeger";
        }
        if (traderId == "6617beeaa9cfa777ca915b7c")
        {
            return "Ref";
        }

        return "BAD_TRADER_ID";
    }
}