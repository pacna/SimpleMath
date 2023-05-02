using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Constants;

namespace SimpleMath.MonoGame.Components.Buttons;

public class SubmitButton : BaseButton
{
    public SubmitButton(GraphicsDevice graphicsDevice, SpriteFont font) : base(graphicsDevice: graphicsDevice, font: font)
    {
    }

    protected override Color BackgroundColor => CustomColor.SanFelix;
    public override int Width => 120 + (int)BoxModel.BorderWidth;
}