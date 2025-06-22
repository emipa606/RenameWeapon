using RimWorld;
using Verse;

namespace RenameGun;

public static class GenLabelFixed
{
    public static string ThingLabel(string name, Thing t, int stackCount, bool includeStuff, bool includeQuality,
        bool includeHP)
    {
        return newThingLabel(name, t, stackCount, includeStuff, includeQuality, includeHP);
    }

    private static string thingLabel(string fixedName, ThingDef stuffDef, int stackCount = 1)
    {
        return newThingLabel(fixedName, stuffDef, stackCount);
    }

    private static string newThingLabel(string fixedName, ThingDef stuffDef, int stackCount)
    {
        string text = stuffDef != null
            ? "ThingMadeOfStuffLabel".Translate(stuffDef.LabelAsStuff, fixedName)
            : fixedName;
        if (stackCount > 1)
        {
            text = $"{text} x{stackCount.ToStringCached()}";
        }

        return text;
    }

    private static string newThingLabel(string name, Thing t, int stackCount, bool includeStuff, bool includeQuality,
        bool includeHp)
    {
        var styleDef = t.StyleDef;
        var text = styleDef == null || styleDef.overrideLabel.NullOrEmpty()
            ? thingLabel(name, includeStuff ? t.Stuff : null)
            : styleDef.overrideLabel;
        var tryGetQuality = t.TryGetQuality(out var qc) && includeQuality;
        var hitPoints = t.HitPoints;
        var maxHitPoints = t.MaxHitPoints;
        var hitPointsLow = t.def.useHitPoints && hitPoints < maxHitPoints && t.def.stackLimit == 1 && includeHp;
        var wornByCorpse = (t as Apparel)?.WornByCorpse ?? false;
        if (tryGetQuality || hitPointsLow || wornByCorpse)
        {
            text += " (";
            if (tryGetQuality)
            {
                text += qc.GetLabel();
            }

            if (hitPointsLow)
            {
                if (tryGetQuality)
                {
                    text += " ";
                }

                text += (hitPoints / (float)maxHitPoints).ToStringPercent();
            }

            if (wornByCorpse)
            {
                if (tryGetQuality || hitPointsLow)
                {
                    text += " ";
                }

                text += "WornByCorpseChar".Translate();
            }

            text += ")";
        }

        if (stackCount > 1)
        {
            text = $"{text} x{stackCount.ToStringCached()}";
        }

        return text;
    }
}