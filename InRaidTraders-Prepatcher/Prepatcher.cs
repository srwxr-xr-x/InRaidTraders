using Mono.Cecil;
using System.Collections.Generic;
using System;
using BepInEx.Logging;
using System.Diagnostics;
using FieldAttributes = Mono.Cecil.FieldAttributes;

public static class InRaidTradersPatcher
{
    public static IEnumerable<string> TargetDLLs { get; } = new string[] { "Assembly-CSharp.dll" };
    public static TypeDefinition EftScreenEnum;

    public static void Patch(ref AssemblyDefinition assembly)
    {
        try
        {
            EftScreenEnum = assembly.MainModule.GetType("EFT.UI.Screens.EEftScreenType");
            FieldDefinition tradingDefinition = new FieldDefinition("Trading", FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.HasDefault, EftScreenEnum) { Constant = 47};
            EftScreenEnum.Fields.Add(tradingDefinition);
            
            Logger.CreateLogSource("InRaidTraders PrePatch").LogInfo("Patching Complete!");
        }
        catch (Exception ex)
        {
            var stackTrace = new StackTrace(ex, true);
            var topStackFrame = stackTrace.GetFrame(0);
            var lineNumber = topStackFrame.GetFileLineNumber();

            Logger.CreateLogSource("Skills Extended PrePatch")
                .LogError("Error When Patching: " + ex.Message + " - Line " + lineNumber);
        }
    }
}
    
  