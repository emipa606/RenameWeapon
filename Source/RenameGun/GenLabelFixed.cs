using RimWorld;
using Verse;

namespace RenameGun;

public static class GenLabelFixed
{
    public static string ThingLabel(string name, Thing t, int stackCount, bool includeStuff, bool includeQuality,
        bool includeHP)
    {
        //var key = default(LabelRequest);
        //key.entDef = t.def;
        //key.styleDef = t.StyleDef;
        //key.stackCount = stackCount;
        //key.stuffDef = includeStuff ? t.Stuff : null;
        //key.hasQuality = includeQuality && t.TryGetQuality(out key.quality);
        //if (t.def.useHitPoints && includeHP)
        //{
        //    key.health = t.HitPoints;
        //    key.maxHealth = t.MaxHitPoints;
        //}

        //if (t is Apparel)
        //{
        //    key.wornByCorpse = apparel.WornByCorpse;
        //}

        return NewThingLabel(name, t, stackCount, includeStuff, includeQuality, includeHP);
    }

    public static string ThingLabel(string fixedName, BuildableDef entDef, ThingDef stuffDef, int stackCount = 1)
    {
        //var key = default(LabelRequest);
        //key.entDef = entDef;
        //key.stuffDef = stuffDef;
        //key.stackCount = stackCount;
        return NewThingLabel(fixedName, stuffDef, stackCount);
    }

    private static string NewThingLabel(string fixedName, ThingDef stuffDef, int stackCount)
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

    private static string NewThingLabel(string name, Thing t, int stackCount, bool includeStuff, bool includeQuality,
        bool includeHp)
    {
        var styleDef = t.StyleDef;
        var text = styleDef == null || styleDef.overrideLabel.NullOrEmpty()
            ? ThingLabel(name, t.def, includeStuff ? t.Stuff : null)
            : styleDef.overrideLabel;
        var tryGetQuality = t.TryGetQuality(out var qc) && includeQuality;
        var hitPoints = t.HitPoints;
        var maxHitPoints = t.MaxHitPoints;
        var hitpointsLow = t.def.useHitPoints && hitPoints < maxHitPoints && t.def.stackLimit == 1 && includeHp;
        var wornByCorpse = (t as Apparel)?.WornByCorpse ?? false;
        if (tryGetQuality || hitpointsLow || wornByCorpse)
        {
            text += " (";
            if (tryGetQuality)
            {
                text += qc.GetLabel();
            }

            if (hitpointsLow)
            {
                if (tryGetQuality)
                {
                    text += " ";
                }

                text += (hitPoints / (float)maxHitPoints).ToStringPercent();
            }

            if (wornByCorpse)
            {
                if (tryGetQuality || hitpointsLow)
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