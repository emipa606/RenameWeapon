using HarmonyLib;
using RimWorld;
using Verse;

namespace RenameGun;

[StaticConstructorOnStartup]
public static class RenameGunStartup
{
    static RenameGunStartup()
    {
        if (RenameGunSettings.HoldingPeriodInDaysForAutoRename > -1)
        {
            var oldSettingTicks = (int)(RenameGunSettings.HoldingPeriodInDaysForAutoRename * GenDate.TicksPerDay);
            RenameGunSettings.HoldingPeriodInDaysForAutoRenameRange = new IntRange(oldSettingTicks, oldSettingTicks);
            RenameGunSettings.HoldingPeriodInDaysForAutoRename = -1;
            RenameGunMod.Settings.Write();
        }

        foreach (var thingDef in DefDatabase<ThingDef>.AllDefs)
        {
            if (!thingDef.IsWeapon)
            {
                continue;
            }

            thingDef.comps ??= [];

            thingDef.comps.Add(new CompProperties_FixedName());
        }

        new Harmony("RenameGun.Mod").PatchAll();
    }
}