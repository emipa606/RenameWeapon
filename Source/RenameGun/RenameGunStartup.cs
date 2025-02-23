using HarmonyLib;
using RimWorld;
using Verse;

namespace RenameGun;

[StaticConstructorOnStartup]
public static class RenameGunStartup
{
    static RenameGunStartup()
    {
        if (RenameGunSettings.holdingPeriodInDaysForAutoRename > -1)
        {
            var oldSettingTicks = (int)(RenameGunSettings.holdingPeriodInDaysForAutoRename * GenDate.TicksPerDay);
            RenameGunSettings.holdingPeriodInDaysForAutoRenameRange = new IntRange(oldSettingTicks, oldSettingTicks);
            RenameGunSettings.holdingPeriodInDaysForAutoRename = -1;
            RenameGunMod.settings.Write();
        }

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