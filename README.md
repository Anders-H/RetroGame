# RetroGame
Support classes for creating retro games using MonoGame.

Use project template: MonoGame Windows Desktop Application (OpenGL).

## Getting started
1. Make sure you have MonoGame extension for Visual Studio 2022 installed, version 3.8 or higher.

2. Create a MonoGame project of type "MonoGame Windows Desktop Application". Target framework should be .NET 8.0 for Windows (net8.0-windows).

3. Add a reference to RetroGame. (Nuget package not yet available.)

4. As shown in included examples, your game should inherit the `RetroGame` class in the RetroGame library and override the `LoadContent` method to load content and set startscene.

5. Select a scene by assigning a value to the `CurrentScene` property. Assigning a different instance calls its `BeginScene()` immediately after assignment; reassigning the same instance does nothing. Scene ticks are retained when reselected; use `ResetTicks()` to restart them.

6. Scenes should inherit the `Scene` class and override methods `Update` and `Draw`.

The `Update` method must call its base method for auto updating to occur: `base.Update(gameTime, ticks);`

The `Draw` method must call its base method for auto drawing to occur: `base.Draw(gameTime, ticks, spriteBatch);`

## Rendering and loading

The built-in C64 font, including Swedish characters, is embedded in RetroGame.dll. Text rendering loads it automatically on first use; client projects need no font files, content pipeline entries, or font initialization. One texture is reused per graphics device and disposed with that device.

The logical resolution uses point sampling and scales to fit the back buffer while preserving its aspect ratio. Fullscreen uses the current desktop resolution. Set `Border = true` to reserve a margin of about 5% on each side; `BorderColor` controls the surrounding color.

A `ContentLoaderScene` can display an optional `LoadingTexture`. Load and assign it in the scene constructor, for example `LoadingTexture = parent.Content.Load<Texture2D>("floppy");`. The scene does not load or dispose that texture itself.

## High scores

`HighScoreList` implements `ISceneActor`. Register it with `AddToAutoUpdate(highScores)` for scrolling and blinking, and optionally `AddToAutoDraw(highScores)` for drawing. Existing manual calls to `Draw` still work, but drawing no longer advances animations.

The list retains the best 15 scores. New scores precede existing entries with equal points. `TryAdd(score, name)` returns whether the score qualified; `Add` and `AddNew` remain available. `BeginEdit` edits the new entry and does not start editing if the score fails to qualify. `GetStrings()` returns a copy; rendering uses strings cached when entries change.

## Textures and tilemaps

`RetroTexture.GetCellBounds(index)` resolves cells across every row of an atlas. `PlotCell` requires a bitmap matching the cell size and transfers only that cell; fully transparent pixels preserve existing texture pixels. `CollisionSprite` uses its selected cell for both drawing and collisions and remains a non-animated sprite.

Tilemaps support both scrolling directions and speeds greater than a tile per update, preserving pixel remainders. `Repeat` wraps indices in both directions; without it the map can scroll offscreen. A tileset may have multiple atlas rows; tile size should match texture cell size.

## Tests

Run `dotnet test RetroGame.Tests/RetroGame.Tests.csproj`. The suite includes logic regressions and a graphics integration test that creates a MonoGame DesktopGL device without starting the interactive game loop. The graphics test requires a working OpenGL environment.

## Games

 - [Secret Agent Man](https://github.com/Anders-H/Secret-Agent-Man)
