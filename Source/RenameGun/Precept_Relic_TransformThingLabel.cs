using HarmonyLib;
using RimWorld;
using Verse;

namespace RenameGun;

[HarmonyPatch(typeof(Precept_Relic), nameof(Precept_Relic.TransformThingLabel))]
public static class Precept_Relic_TransformThingLabel
{
    public static bool Prefix(Thing ___generatedRelic, string label, ref string __result)
    {
        var comp = ___generatedRelic?.TryGetComp<CompFixedName>();
        if (comp == null)
        {
            return true;
        }

        if (!comp.overrideRelicName)
        {
            return true;
        }

        __result = label;
        return false;
    }
}