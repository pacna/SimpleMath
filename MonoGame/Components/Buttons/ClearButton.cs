using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Constants;

namespace SimpleMath.MonoGame.Components.Buttons;

public class ClearButton : BaseButton
{
    public ClearButton(GraphicsDevice graphicsDevice, SpriteFont font) : base(graphicsDevice: graphicsDevice, font: font)
    {
    }

    protected override Color BackgroundColor => CustomColor.Tamarillo;
}