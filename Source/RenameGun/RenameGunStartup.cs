using HarmonyLib;
using Verse;

namespace RenameGun;

[StaticConstructorOnStartup]
public static class RenameGunStartup
{
    static RenameGunStartup()
    {
        foreach (var thingDef in DefDatabase<ThingDef>.AllDefs)
        {
            if (!thingDef.IsWeapon)
            {
                continue;
            }

            if (thingDef.comps is null)
            {
                thingDef.comps = [];
            }

            thingDef.comps.Add(new CompProperties_FixedName());
        }

        new Harmony("RenameGun.Mod").PatchAll();
    }
}