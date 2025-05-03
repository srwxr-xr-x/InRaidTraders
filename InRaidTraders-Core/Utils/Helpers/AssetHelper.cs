using System.IO;
using System.Reflection;
using UnityEngine;

namespace InRaidTraders.Helpers;

public class AssetHelper
{
    public static readonly string CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static readonly string AssetsDirectory = $"{CurrentDirectory}\\Assets";

    public static void LoadBundles()
    {
        Assets.Prapor = LoadAndInitializePrefabs("\\Bundles\\prapor.bundle");
    }

    private static AssetBundle LoadAndInitializePrefabs(string bundlePath)
    {
        string fullPath = AssetsDirectory + bundlePath;
        Plugin.LogSource.LogInfo($"Loading assets from {fullPath}");
        AssetBundle assetBundle = AssetBundle.LoadFromFile(fullPath);
        return assetBundle;
    }
}