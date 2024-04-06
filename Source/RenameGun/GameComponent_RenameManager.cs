using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace RenameGun;

public class GameComponent_RenameManager : GameComponent
{
    public static GameComponent_RenameManager Instance;
    public List<CompFixedName> comps;
    public List<Thing> things;

    public GameComponent_RenameManager(Game game)
    {
        Instance = this;
    }

    public void Init()
    {
        things ??= [];
        comps ??= [];
        Instance = this;
    }

    public override void LoadedGame()
    {
        base.LoadedGame();
        Init();
    }

    public override void StartedNewGame()
    {
        base.StartedNewGame();
        Init();
    }

    public override void GameComponentTick()
    {
        base.GameComponentTick();
        if (!RenameGunSettings.allowPawnsToRenameGuns)
        {
            return;
        }

        foreach (var comp in comps)
        {
            if (!comp.fixedName.NullOrEmpty() && RenameGunSettings.alwaysKeepPlayerSetNames)
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
            if (comp.holdingCounter >=
                RenameGunSettings.holdingPeriodInDaysForAutoRename * GenDate.TicksPerDay)
            {
                comp.AutoRename();
            }
        }
    }

    public void TryAddThing(CompFixedName compFixedName)
    {
        Init();
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

        Init();
        things = things.Where(x => x.TryGetComp<CompFixedName>() != null).ToList();
        comps = things.Select(x => x.TryGetComp<CompFixedName>()).ToList();
    }
}