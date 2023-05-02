using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Constants;
using SimpleMath.MonoGame.MessageBus;

namespace SimpleMath.MonoGame.Components;

public abstract class BaseTextField : IRenderer, IHandler
{
    private readonly SpriteFont _font;

    public BaseTextField(SpriteFont font)
    {
        this._font = font;
    }

    protected abstract string Id { get; }

    public Vector2 Position { get; init; }

    protected readonly StringBuilder TextBuilder = new StringBuilder();

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(spriteFont: this._font, text: this.TextBuilder.ToString(), position: this.Position, color: CustomColor.White);
    }

    public abstract void HandleMsg(IMessageDoc msg);
}