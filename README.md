# RetroGame
Support classes for creating retro games using MonoGame.

Use project template: MonoGame Cross Platform Desktop Project.

## Getting started
1. Make sure you have MonoGame installed.

2. Create a MonoGame project of type "MonoGame Cross Platform Desktop Project". Target framework should be 4.6 or higher.

3. As shown in included examples, your game should inherit the `RetroGame` class and override the `LoadContent` method to load content and set startscene.

4. Select a scene by assigning a value to the `CurrentScene` property.

5. Scenes should inherit the `Scene` class and override methods `Update` and `Draw`.
