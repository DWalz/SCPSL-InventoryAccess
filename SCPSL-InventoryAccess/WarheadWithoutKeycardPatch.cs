using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace SCPSL_InventoryAccess
{
    [HarmonyPatch]
    internal class WarheadWithoutKeycardPatch
    {
        private static IEnumerable<MethodBase> TargetMethods()
        {
            var c = AccessTools.TypeByName("Exiled.Events.Patches.Events.Player.ActivatingWarheadPanel");
            var m = AccessTools.Method(c, "Prefix");
            yield return m;
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToArray();

            // Rewrite call for current held item to always return ItemType.Coin to allow the Warhead event to be called.
            // EXILED currently requires the player to hold an item.
            for (var i = 0; i < codes.Length; i++)
            {
                if (codes[i].opcode == OpCodes.Leave &&
                    codes[i + 1].opcode == OpCodes.Ldarg_0 &&
                    codes[i + 2].opcode == OpCodes.Ldfld &&
                    codes[i + 3].opcode == OpCodes.Ldarg_0)
                {
                    yield return codes[i];
                    yield return codes[i + 1];
                    yield return codes[i + 2];
                    yield return new CodeInstruction(OpCodes.Nop);
                    yield return new CodeInstruction(OpCodes.Nop);
                    yield return new CodeInstruction(OpCodes.Ldc_I4, 35); // ItemType.Coin
                    i += 5;
                }
                else
                {
                    yield return codes[i];
                }
            }
        }
    }
}
