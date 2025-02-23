using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using static Verse.Widgets;

namespace RenameGun;

public class RenameGunSettings : ModSettings
{
    public static bool allowPawnsToRenameGuns = true;
    public static bool alwaysKeepPlayerSetNames = true;
    public static float holdingPeriodInDaysForAutoRename = -1;

    public static IntRange holdingPeriodInDaysForAutoRenameRange =
        new IntRange(3 * GenDate.TicksPerDay, 3 * GenDate.TicksPerDay);

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref holdingPeriodInDaysForAutoRename, "holdingPeriodInDaysForAutoRename");
        Scribe_Values.Look(ref holdingPeriodInDaysForAutoRenameRange, "holdingPeriodInDaysForAutoRenameRange");
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

        var rangeRect = listingStandard.GetRect(32f);
        IntRange(rangeRect, 1, ref holdingPeriodInDaysForAutoRenameRange, 0, 60 * GenDate.TicksPerDay,
            "RG.HoldingPeriodInDaysForAutoRenameRange".Translate(
                holdingPeriodInDaysForAutoRenameRange.min.ToStringTicksToPeriod(),
                holdingPeriodInDaysForAutoRenameRange.max.ToStringTicksToPeriod()));

        if (RenameGunMod.currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("RG.CurrentModVersion".Translate(RenameGunMod.currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
    }

    public static void IntRange(Rect rect, int id, ref IntRange range, int min = 0, int max = 100, string label = null,
        int minWidth = 0)
    {
        var rect2 = rect;
        rect2.xMin += 8f;
        rect2.xMax -= 8f;
        GUI.color = RangeControlTextColor;
        var font = Text.Font;
        Text.Font = GameFont.Small;
        Text.Anchor = TextAnchor.UpperCenter;
        var rect3 = rect2;
        rect3.yMin -= 2f;
        Label(rect3, label);
        Text.Anchor = TextAnchor.UpperLeft;
        var position = new Rect(rect2.x, rect2.yMax - 8f - 1f, rect2.width, 2f);
        GUI.DrawTexture(position, BaseContent.WhiteTex);
        var num = rect2.x + (rect2.width * (range.min - min) / (max - min));
        var num2 = rect2.x + (rect2.width * (range.max - min) / (max - min));
        GUI.color = Color.white;
        GUI.DrawTexture(new Rect(num, rect2.yMax - 8f - 2f, num2 - num, 4f), BaseContent.WhiteTex);
        var position2 = new Rect(num - 16f, position.center.y - 8f, 16f, 16f);
        GUI.DrawTexture(position2, FloatRangeSliderTex);
        var position3 = new Rect(num2 + 16f, position.center.y - 8f, -16f, 16f);
        GUI.DrawTexture(position3, FloatRangeSliderTex);
        if (curDragEnd != 0 &&
            (Event.current.type == EventType.MouseUp || Event.current.rawType == EventType.MouseDown))
        {
            draggingId = 0;
            curDragEnd = RangeEnd.None;
            SoundDefOf.DragSlider.PlayOneShotOnCamera();
        }

        var isDragging = false;
        if (Mouse.IsOver(rect) || draggingId == id)
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && id != draggingId)
            {
                draggingId = id;
                var x = Event.current.mousePosition.x;
                if (x < position2.xMax)
                {
                    curDragEnd = RangeEnd.Min;
                }
                else if (x > position3.xMin)
                {
                    curDragEnd = RangeEnd.Max;
                }
                else
                {
                    var num5 = Mathf.Abs(x - position2.xMax);
                    var num6 = Mathf.Abs(x - (position3.x - 16f));
                    curDragEnd = num5 < num6 ? RangeEnd.Min : RangeEnd.Max;
                }

                isDragging = true;
                Event.current.Use();
                SoundDefOf.DragSlider.PlayOneShotOnCamera();
            }

            if (isDragging || curDragEnd != 0 && UnityGUIBugsFixer.MouseDrag())
            {
                var num7 = Mathf.RoundToInt(Mathf.Clamp(
                    ((Event.current.mousePosition.x - rect2.x) / rect2.width * (max - min)) + min, min, max));
                if (curDragEnd == RangeEnd.Min)
                {
                    if (num7 != range.min)
                    {
                        range.min = num7;
                        if (range.min > max - minWidth)
                        {
                            range.min = max - minWidth;
                        }

                        var num8 = Mathf.Max(min, range.min + minWidth);
                        if (range.max < num8)
                        {
                            range.max = num8;
                        }

                        CheckPlayDragSliderSound();
                    }
                }
                else if (curDragEnd == RangeEnd.Max && num7 != range.max)
                {
                    range.max = num7;
                    if (range.max < min + minWidth)
                    {
                        range.max = min + minWidth;
                    }

                    var num9 = Mathf.Min(max, range.max - minWidth);
                    if (range.min > num9)
                    {
                        range.min = num9;
                    }

                    CheckPlayDragSliderSound();
                }

                if (Event.current.type == EventType.MouseDrag)
                {
                    Event.current.Use();
                }
            }
        }

        Text.Font = font;
    }
}