using Mlie;
using UnityEngine;
using Verse;

namespace RenameGun;

public class RenameGunMod : Mod
{
    public static RenameGunSettings Settings;
    public static string CurrentVersion;

    public RenameGunMod(ModContentPack pack) : base(pack)
    {
        Settings = GetSettings<RenameGunSettings>();
        CurrentVersion = VersionFromManifest.GetVersionFromModMetaData(pack.ModMetaData);
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        base.DoSettingsWindowContents(inRect);
        RenameGunSettings.DoSettingsWindowContents(inRect);
    }

    public override string SettingsCategory()
    {
        return Content.Name;
    }
}