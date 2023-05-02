using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Components.Buttons;

namespace SimpleMath.MonoGame.Components;

public static class ButtonFactory
{
    public static BaseButton CreateBasicButton(GraphicsDevice graphicsDevice, SpriteFont font, string text, Vector2 position)
    {
        return new BasicButton(graphicsDevice: graphicsDevice, font: font)
        {
            Text = text,
            Position = position
        };
    }

    public static BaseButton CreateClearButton(GraphicsDevice graphicsDevice, SpriteFont font, string text, Vector2 position)
    {
        return new ClearButton(graphicsDevice: graphicsDevice, font: font)
        {
            Text = text,
            Position = position
        };
    }

    public static BaseButton CreateSubmitButton(GraphicsDevice graphicsDevice, SpriteFont font, string text, Vector2 position)
    {
        return new SubmitButton(graphicsDevice: graphicsDevice, font: font)
        {
            Text = text,
            Position = position
        };
    }
}