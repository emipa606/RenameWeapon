using RimWorld;
using Verse;

namespace RenameGun;

public class CompFixedName : ThingComp
{
    public string colonistSetName;
    public Pawn curPawnHolder;
    public string fixedName;
    public int holdingCounter;

    public bool includeHP = true;
    public bool includeQuality = true;
    public bool includeStuff = true;
    public CompProperties_FixedName Props => props as CompProperties_FixedName;

    public Pawn HoldingPawn
    {
        get
        {
            switch (parent.ParentHolder)
            {
                case Pawn_EquipmentTracker eq:
                    return eq.pawn;
                case Pawn_InventoryTracker inv:
                    return inv.pawn;
                default:
                    return null;
            }
        }
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        base.PostSpawnSetup(respawningAfterLoad);
        GameComponent_RenameManager.Instance.TryAddThing(this);
    }

    public override void PostDestroy(DestroyMode mode, Map previousMap)
    {
        base.PostDestroy(mode, previousMap);
        GameComponent_RenameManager.Instance.RemoveThing(this);
    }

    public override string TransformLabel(string label)
    {
        if (!colonistSetName.NullOrEmpty())
        {
            return colonistSetName;
        }

        if (!fixedName.NullOrEmpty())
        {
            return GenLabelFixed.ThingLabel(fixedName, parent, parent.stackCount, includeStuff, includeQuality,
                includeHP);
        }

        return base.TransformLabel(label);
    }

    public void AutoRename()
    {
        var pawn = HoldingPawn;
        var taleRef = Find.TaleManager.GetRandomTaleReferenceForArtConcerning(parent);
        var oldName = GenLabelFixed.ThingLabel(fixedName ?? parent.def.label, parent, parent.stackCount, includeStuff,
            false, false);
        colonistSetName =
            GenText.CapitalizeAsTitle(taleRef.GenerateText(TextGenerationPurpose.ArtName, RG_DefOf.NamerArtWeaponGun));
        if (!PawnUtility.ShouldSendNotificationAbout(pawn))
        {
            return;
        }

        var newName =
            GenLabelFixed.ThingLabel(colonistSetName, parent, parent.stackCount, includeStuff, false, false);
        Messages.Message("RG.PawnRenamedGunMessage".Translate(pawn.Named("PAWN"), oldName, newName), pawn,
            MessageTypeDefOf.PositiveEvent);
    }

    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref fixedName, "fixedName");
        Scribe_Values.Look(ref colonistSetName, "colonistSetName");
        Scribe_Values.Look(ref includeHP, "includeHP", true);
        Scribe_Values.Look(ref includeQuality, "includeQuality", true);
        Scribe_Values.Look(ref includeStuff, "includeStuff", true);
        Scribe_Values.Look(ref holdingCounter, "holdingCounter");
        Scribe_References.Look(ref curPawnHolder, "curPawnHolder");
        if (Scribe.mode == LoadSaveMode.PostLoadInit)
        {
            GameComponent_RenameManager.Instance.TryAddThing(this);
        }
    }
}