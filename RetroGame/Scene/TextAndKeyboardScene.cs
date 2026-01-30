using RetroGame.Input;
using RetroGame.Text;

namespace RetroGame.Scene;

public class TextAndKeyboardScene : Scene
{
    public readonly TextBlock TextBlock;
    public readonly KeyboardStateChecker Keyboard;

    public TextAndKeyboardScene(RetroGame parent) : base(parent)
    {
        TextBlock = new TextBlock(CharacterSet.Uppercase);
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
    }
}