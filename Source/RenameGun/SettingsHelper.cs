using UnityEngine;
using Verse;
using static Verse.Widgets;

namespace RenameGun;

internal static class SettingsHelper
{
    public static void SliderLabeled(this Listing_Standard ls, string label, ref float val, string format,
        float min = 0f, float max = 1f, string tooltip = null)
    {
        var rect = ls.GetRect(Text.LineHeight);
        var rect2 = rect.LeftPart(0.7f).Rounded();
        var rect3 = rect.RightPart(0.3f).Rounded().LeftPart(0.67f).Rounded();
        var rect4 = rect.RightPart(0.1f).Rounded();
        var anchor = Text.Anchor;
        Text.Anchor = TextAnchor.MiddleLeft;
        Label(rect2, label);
        var num = HorizontalSlider(rect3, val, min, max, true);
        val = num;
        Text.Anchor = TextAnchor.MiddleRight;
        Label(rect4, string.Format(format, val));
        if (!tooltip.NullOrEmpty())
        {
            TooltipHandler.TipRegion(rect, tooltip);
        }

        Text.Anchor = anchor;
        ls.Gap(ls.verticalSpacing);
    }
}