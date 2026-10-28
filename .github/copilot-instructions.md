.github/copilot-instructions.md
# Rename Weapon (Continued) GitHub Copilot Instructions

## Mod Overview and Purpose

Rename Weapon (Continued) is an update to the Taranchuk and Halituis Amaricanous mod that allows players to rename guns, melee weapons, and other items in RimWorld. The mod adds a new UI element to the weapon select UI and allows players to rename their gun of choice into 27 characters of whatever they desire. The mod also includes the option to override relic names.

## Key Features and Systems

- **New UI Element**: A new UI element is added to the weapon select UI that allows players to rename their weapons.
- **Rename Gun of Choice**: Players can rename their gun of choice into 27 characters of whatever they desire.
- **Override Relic Names**: Added option to override relic-names.
- **Disable HP / Thing Prefixes / Suffix Quality**: Players can disable HP, Thing prefixes, or Suffix Quality.
- **Pawns Rename Weapons**: Pawns will rename weapons on their own after using them for a given time (adjustable time in mod settings).

## Coding Patterns and Conventions

The mod follows standard RimWorld coding patterns and conventions. The C# files are organized into classes that handle specific functionalities, such as renaming guns, settings, and startup. The XML file is used to define mod metadata and dependencies.

## XML Integration

The mod uses the About.xml file to define mod metadata and dependencies. The file is structured as follows:

xml
<About>
  <Name>Rename Weapon (Continued)</Name>
  <Author>Taranchuk, Halituis Amaricanous</Author>
  <Version>1.0.0</Version>
  <Dependencies>
    <Dependency>brrainz.harmony</Dependency>
  </Dependencies>
</About>


## Harmony Patching

The mod uses Harmony patching to modify the behavior of RimWorld. Harmony is a popular library for patching and modding RimWorld. The mod includes the following Harmony patches:

- `CompFixedName`: Handles the renaming of weapons and other items.
- `RenameGunSettings`: Handles the mod settings.
- `RenameGunStartup`: Handles the startup of the mod.

## Suggestions for Copilot

- **Code Generation**: Use Copilot to generate code for new features or bug fixes.
- **XML File Generation**: Use Copilot to generate XML files for mod metadata and dependencies.
- **Harmony Patching**: Use Copilot to generate Harmony patches for modifying RimWorld behavior.
- **UI Element Generation**: Use Copilot to generate code for new UI elements.

## Conclusion

Rename Weapon (Continued) is a popular mod for RimWorld that allows players to rename guns, melee weapons, and other items. The mod follows standard RimWorld coding patterns and conventions and uses Harmony patching to modify RimWorld behavior. Copilot can be used to generate code, XML files, Harmony patches, and UI elements for the mod.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

