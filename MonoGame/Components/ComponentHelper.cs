using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Constants;

namespace SimpleMath.MonoGame.Components;

public static class ComponentHelper
{
    public static Texture2D CreateTexture(GraphicsDevice graphicsDevice)
    {
        Texture2D texture = new(graphicsDevice: graphicsDevice, width: 1, height: 1);
        texture.SetData<Color>(data: new[] { CustomColor.White});

        return texture;
    }
}