using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Components;

namespace SimpleMath.MonoGame;

public class GUICalculator
{

    private IEnumerable<IComponent> _buttons;

    private GUICalculator()
    {
    }

    public BaseTextField IO;
    public BaseTextField PostFix;

    public void LoadButtons(params IComponent[] buttons)
    {
        this._buttons = buttons;
    }

    public void LoadTextFields(BaseTextField io, BaseTextField postFix)
    {
        this.IO = io;
        this.PostFix = postFix;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(IComponent button in this._buttons)
        {
            button.Draw(spriteBatch: spriteBatch);
        }

        this.IO.Draw(spriteBatch: spriteBatch);
        this.PostFix.Draw(spriteBatch: spriteBatch);
    }

    public void Update()
    {
        foreach(IComponent button in this._buttons)
        {
            button.Update();
        }
    }

    public static GUICalculator Instance => new();
}