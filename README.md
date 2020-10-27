# RetroGame
Support classes for creating retro games using MonoGame.

Use project template: MonoGame Cross Platform Desktop Project.

## Getting started
1. Make sure you have [MonoGame extension for Visual Studio 2019](https://docs.monogame.net/articles/getting_started/1_setting_up_your_development_environment_windows.html) installed, and the correct tools

2. Create a MonoGame project of type "MonoGame Cross-Platform Desktop Application (OpenGL)". Target framework should be 4.8 or higher.

3. Add a reference to RetroGame. (Nuget package not yet available.)

4. As shown in included examples, your game should inherit the `RetroGame` class in the RetroGame library and override the `LoadContent` method to load content and set startscene.

5. Select a scene by assigning a value to the `CurrentScene` property.

6. Scenes should inherit the `Scene` class and override methods `Update` and `Draw`.

The `Update` method must call its base method for auto updating to occur: `base.Update(gameTime, ticks);`

The `Draw` method must call its base method for auto drawing to occur: `base.Draw(gameTime, ticks, spriteBatch);`
