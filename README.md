# RetroGame
Support classes for creating retro games using MonoGame.

Use project template: MonoGame Windows Desktop Application (MonoGame Team).

## Getting started
1. Make sure you have MonoGame extension for Visual Studio 2022 installed, version 3.8 or higher.

2. Create a MonoGame project of type "MonoGame Windows Desktop Application". Target framework should be .NET 8.0 for Windows (net8.0-windows).

3. Add a reference to RetroGame. (Nuget package not yet available.)

4. As shown in included examples, your game should inherit the `RetroGame` class in the RetroGame library and override the `LoadContent` method to load content and set startscene.

5. Select a scene by assigning a value to the `CurrentScene` property.

6. Scenes should inherit the `Scene` class and override methods `Update` and `Draw`.

The `Update` method must call its base method for auto updating to occur: `base.Update(gameTime, ticks);`

The `Draw` method must call its base method for auto drawing to occur: `base.Draw(gameTime, ticks, spriteBatch);`

## Games

 - [Secret Agent Man](https://github.com/Anders-H/Secret-Agent-Man)
