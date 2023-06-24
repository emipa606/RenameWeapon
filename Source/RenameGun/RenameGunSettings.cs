using UnityEngine;
using Verse;

namespace RenameGun;

public class RenameGunSettings : ModSettings
{
    public static bool allowPawnsToRenameGuns = true;
    public static bool alwaysKeepPlayerSetNames = true;
    public static float holdingPeriodInDaysForAutoRename = 3f;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref holdingPeriodInDaysForAutoRename, "holdingPeriodInDaysForAutoRename");
        Scribe_Values.Look(ref allowPawnsToRenameGuns, "allowPawnsToRenameGuns");
        Scribe_Values.Look(ref alwaysKeepPlayerSetNames, "alwaysKeepPlayerSetNames");
    }

    public void DoSettingsWindowContents(Rect inRect)
    {
        var rect = new Rect(inRect.x, inRect.y, inRect.width, inRect.height);
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        listingStandard.CheckboxLabeled("RG.AllowColonistsToRenameGuns".Translate(), ref allowPawnsToRenameGuns);
        listingStandard.CheckboxLabeled("RG.AlwaysKeepPlayerSetNames".Translate(), ref alwaysKeepPlayerSetNames);
        listingStandard.SliderLabeled("RG.HoldingPeriodInDaysForAutoRename".Translate(),
            ref holdingPeriodInDaysForAutoRename,
            "PeriodDays".Translate(holdingPeriodInDaysForAutoRename.ToStringDecimalIfSmall()), 0, 60);
        if (RenameGunMod.currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("RG.CurrentModVersion".Translate(RenameGunMod.currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }
}