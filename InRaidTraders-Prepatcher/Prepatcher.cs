using Mono.Cecil;
using System.Collections.Generic;
using System;
using BepInEx.Logging;
using System.Diagnostics;
using System.Reflection;
using FieldAttributes = Mono.Cecil.FieldAttributes;

public static class InRaidTradersPatcher
{
    public static IEnumerable<string> TargetDLLs { get; } = new string[] { "Assembly-CSharp.dll" };
    public static TypeDefinition TraderDialogEnum;

    public static void Patch(ref AssemblyDefinition assembly)
    {
        try
        {
            TraderDialogEnum = assembly.MainModule.GetType("EFT.Trading.ETraderDialogType");
            FieldDefinition tradingDefinition = new FieldDefinition("Trading", FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.HasDefault, TraderDialogEnum) { Constant = -7};
            TraderDialogEnum.Fields.Add(tradingDefinition);
            
            foreach (var traderField in TraderDialogEnum.Fields)
            {
                Logger.CreateLogSource("InRaidTraders PrePatch").LogInfo(traderField.Name);

            }
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
    
  