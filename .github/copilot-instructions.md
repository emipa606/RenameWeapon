# GitHub Copilot Instructions for Rename Weapon (Continued) RimWorld Mod

## Mod Overview and Purpose
Rename Weapon (Continued) is an update to the Taranchuk and Halituis Amaricanous mod, which allows players to rename their weapons, melee weapons, and other items within the game. The mod provides a new UI element in the weapon select UI, enabling users to rename weapons to up to 27 characters of their choice. Additionally, it offers settings to disable HP, Thing prefixes, and suffix quality. Pawns can also rename weapons they use after a specified time, adjustable in the mod settings.

## Key Features and Systems
- **UI Integration**: A new UI element is added to the weapon select UI, allowing users to rename weapons.
- **Weapon Renaming**: Allows renaming of weapons, melee weapons, and other items.
- **Settings**: Users can disable HP, Thing prefixes, and suffix quality through mod settings.
- **Pawn Behavior**: Pawns can rename weapons they use after a given time, adjustable in the mod settings.
- **Relic Name Override**: Added an option to override relic names.
- **Label Generation**: An item can have no `StyleDef`. Manual and automatic rename labels use the custom name when there is no style label override, preserving the selected material, quality, and HP options.

## Coding Patterns and Conventions
- **C#**: The mod uses standard C# coding practices with classes, methods, and properties.
- **Harmony Patches**: Utilizes Harmony to patch existing game code for functionality.
- **XML**: Uses XML for mod metadata and integration.

## XML Integration
The mod includes an `About.xml` file that specifies the mod's metadata and dependencies. This file is essential for mod integration and should be placed in the appropriate directory within the mod's structure.

## Harmony Patching
Rename Weapon (Continued) uses Harmony to patch existing game code to integrate its features. Key patches include:
- Patches for renaming functionality.
- Patches to modify weapon UI behavior.
- Patches to handle pawn behavior for weapon renaming.

## Suggestions for Copilot
- **Code Generation**: Use Copilot to generate code for UI elements, settings windows, and Harmony patches.
- **XML Templates**: Use Copilot to generate XML templates for mod metadata and integration.
- **Testing and Debugging**: Use Copilot to generate test cases and debugging scripts to ensure mod functionality.
- **Documentation**: Use Copilot to generate documentation for the mod, including code comments and user guides.

## Example C# Code Snippets
### CompFixedName.cs
csharp
using System;
using Verse;
using RimWorld;

namespace RenameGun
{
    public class CompFixedName : ThingComp
    {
        public CompProperties_FixedName Props => (CompProperties_FixedName)props;

        public void AutoRename()
        {
            // Implementation for auto-renaming
        }

        public void TransformLabel()
        {
            // Implementation for transforming weapon labels
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            // Additional setup logic
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            // Data exposure logic
        }

        public override void PostDestroy(DestroyMode mode, Map map)
        {
            base.PostDestroy(mode, map);
            // Destruction logic
        }
    }
}


### RenameGunSettings.cs
csharp
using Verse;
using RimWorld;

namespace RenameGun
{
    public class RenameGunSettings : ModSettings
    {
        public IntRange intRange = new IntRange(1, 100);

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            // Implementation for settings window contents
        }

        public override void ExposeData()
        {
            base.ExposeData();
            // Data exposure logic
        }
    }
}


## Example XML Snippet
### About.xml
xml
<About>
    <Name>Rename Weapon (Continued)</Name>
    <Author>Taranchuk, Halituis Amaricanous</Author>
    <Description>
        Allows renaming of weapons, melee weapons, and other items within the game.
        Adds a new UI element to the weapon select UI for renaming.
        Supports settings to disable HP, Thing prefixes, and suffix quality.
        Pawns can rename weapons they use after a given time.
    </Description>
    <Version>1.0.0</Version>
    <Dependencies>
        <Dependency>brrainz.harmony</Dependency>
    </Dependencies>
</About>


This .github/copilot-instructions.md file provides a comprehensive guide for using GitHub Copilot to assist in developing the Rename Weapon (Continued) RimWorld mod. It covers the mod's features, coding patterns, XML integration, Harmony patching, and suggestions for using Copilot effectively.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Temporary files

- Do not create temporary files, build output, test artifacts, or Copilot session
  folders inside the mod root.
- Store temporary artifacts under `%USERPROFILE%\CopilotTemp\[modname]`.
- Direct build and test output there as well, without changing project settings.
- Clean up task-specific temporary artifacts when finished.

## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

