using RimWorld;
using UnityEngine;
using Verse;

namespace RenameGun;

public class Dialog_RenameGun : Window
{
    private readonly Thing gun;
    private string curName;

    private bool focusedRenameField;
    private int startAcceptingInputAtFrame;

    public Dialog_RenameGun(Thing gun)
    {
        forcePause = true;
        doCloseX = true;
        absorbInputAroundWindow = true;
        closeOnAccept = false;
        closeOnClickedOutside = true;
        this.gun = gun;
        var comp = gun.TryGetComp<CompFixedName>();
        curName = comp.fixedName ?? gun.def.label;
    }

    public override Vector2 InitialSize => new(280f, 260f);
    private bool AcceptsInput => startAcceptingInputAtFrame <= Time.frameCount;

    private static int MaxNameLength => 28;

    public void WasOpenedByHotkey()
    {
        startAcceptingInputAtFrame = Time.frameCount + 1;
    }

    private static AcceptanceReport nameIsValid(string name)
    {
        return name.Length != 0;
    }


    public override void DoWindowContents(Rect inRect)
    {
        Text.Font = GameFont.Small;
        var returnPressed = false;
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            returnPressed = true;
            Event.current.Use();
        }

        var rectTitle = new Rect(0, 0, inRect.width, 24);
        Widgets.Label(rectTitle, "RG.Rename".Translate(gun.def.LabelCap));
        GUI.SetNextControlName("RenameField");
        var renameRect = new Rect(0f, 24f, inRect.width, 35f);
        var text = Widgets.TextField(renameRect, curName);
        switch (AcceptsInput)
        {
            case true when text.Length < MaxNameLength:
                curName = text;
                break;
            case false:
                ((TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl)).SelectAll();
                break;
        }

        if (!focusedRenameField)
        {
            UI.FocusControl("RenameField", this);
            focusedRenameField = true;
        }

        var comp = gun.TryGetComp<CompFixedName>();
        Widgets.CheckboxLabeled(new Rect(0f, renameRect.yMax + 10, inRect.width - 4, 24f), "RG.IncludeHP".Translate(),
            ref comp.includeHP);
        Widgets.CheckboxLabeled(new Rect(0f, renameRect.yMax + 10 + 28, inRect.width - 4, 24f),
            "RG.IncludeStuff".Translate(), ref comp.includeStuff);
        Widgets.CheckboxLabeled(new Rect(0f, renameRect.yMax + 10 + 28 + 28, inRect.width - 4, 24f),
            "RG.IncludeQuality".Translate(), ref comp.includeQuality);
        if (ModLister.IdeologyInstalled && gun.StyleSourcePrecept != null)
        {
            Widgets.CheckboxLabeled(new Rect(0f, renameRect.yMax + 10 + 28 + 28 + 28, inRect.width - 4, 24f),
                "RG.OverrideRelicName".Translate(), ref comp.overrideRelicName);
        }

        var okRect = new Rect(15f, inRect.height - 35f, inRect.width - 15f - 15f, 35f);
        if (!(Widgets.ButtonText(okRect, "OK") || returnPressed))
        {
            return;
        }

        var acceptanceReport = nameIsValid(curName);
        if (!acceptanceReport.Accepted)
        {
            if (acceptanceReport.Reason.NullOrEmpty())
            {
                Messages.Message("NameIsInvalid".Translate(), MessageTypeDefOf.RejectInput, false);
            }
            else
            {
                Messages.Message(acceptanceReport.Reason, MessageTypeDefOf.RejectInput, false);
            }
        }
        else
        {
            setName(curName);
            Find.WindowStack.TryRemove(this);
        }
    }

    private void setName(string name)
    {
        var comp = gun.TryGetComp<CompFixedName>();
        comp.fixedName = name;
        if (!RenameGunSettings.AllowPawnsToRenameGuns)
        {
            comp.colonistSetName = string.Empty;
        }
    }
}