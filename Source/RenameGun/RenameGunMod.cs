using Mlie;
using UnityEngine;
using Verse;

namespace RenameGun;

public class RenameGunMod : Mod
{
    public static RenameGunSettings settings;
    public static string currentVersion;

    public RenameGunMod(ModContentPack pack) : base(pack)
    {
        settings = GetSettings<RenameGunSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(pack.ModMetaData);
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        settings.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return Content.Name;
    }
}