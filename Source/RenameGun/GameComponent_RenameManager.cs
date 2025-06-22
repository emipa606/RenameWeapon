using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RenameGun;

public class GameComponent_RenameManager : GameComponent
{
    public static GameComponent_RenameManager Instance;
    private List<CompFixedName> comps;
    private List<Thing> things;

    public GameComponent_RenameManager(Game game)
    {
        Instance = this;
    }

    private void init()
    {
        things ??= [];
        comps ??= [];
        Instance = this;
    }

    public override void LoadedGame()
    {
        base.LoadedGame();
        init();
    }

    public override void StartedNewGame()
    {
        base.StartedNewGame();
        init();
    }

    public override void GameComponentTick()
    {
        base.GameComponentTick();
        if (!RenameGunSettings.AllowPawnsToRenameGuns)
        {
            return;
        }

        foreach (var comp in comps)
        {
            if (!comp.fixedName.NullOrEmpty() && RenameGunSettings.AlwaysKeepPlayerSetNames)
            {
                continue;
            }

            if (!comp.colonistSetName.NullOrEmpty())
            {
                continue;
            }

            var holdingPawn = comp.HoldingPawn;
            if (holdingPawn == null)
            {
                continue;
            }

            if (holdingPawn != comp.curPawnHolder)
            {
                comp.curPawnHolder = holdingPawn;
                comp.holdingCounter = 0;
            }

            comp.holdingCounter++;
            var pawnTimer = Rand.RangeInclusiveSeeded(RenameGunSettings.HoldingPeriodInDaysForAutoRenameRange.min,
                RenameGunSettings.HoldingPeriodInDaysForAutoRenameRange.max, holdingPawn.GetHashCode());

            if (comp.holdingCounter > pawnTimer)
            {
                comp.AutoRename();
            }
        }
    }

    public void TryAddThing(CompFixedName compFixedName)
    {
        init();
        if (!things.Contains(compFixedName.parent))
        {
            things.Add(compFixedName.parent);
        }

        comps = things.Select(x => x.TryGetComp<CompFixedName>()).ToList();
    }

    public void RemoveThing(CompFixedName compFixedName)
    {
        things.Remove(compFixedName.parent);
        comps = things.Select(x => x.TryGetComp<CompFixedName>()).ToList();
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Instance = this;
        Scribe_Collections.Look(ref things, "things", LookMode.Reference);
        if (Scribe.mode != LoadSaveMode.PostLoadInit)
        {
            return;
        }

        init();
        things = things.Where(x => x.TryGetComp<CompFixedName>() != null).ToList();
        comps = things.Select(x => x.TryGetComp<CompFixedName>()).ToList();
    }
}