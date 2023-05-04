using System.ComponentModel.DataAnnotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleMath.MonoGame.Constants;

namespace SimpleMath.MonoGame.Components.Buttons;

public abstract class BaseButton : IComponent
{
    private readonly Texture2D _texture;
    private readonly SpriteFont _font;
    private bool _hasClicked = false;

    public BaseButton(GraphicsDevice graphicsDevice, SpriteFont font)
    {
        this._texture = ComponentHelper.CreateTexture(graphicsDevice: graphicsDevice);
        this._font = font;
    }

    protected virtual Color BackgroundColor => CustomColor.SecondaryMineShaft;

    protected virtual Color TextColor => CustomColor.White;

    protected Rectangle Rect => new(x: (int)this.Position.X, y: (int)this.Position.Y, width: this.Width, height: this.Height);

    public virtual int Width => 60;

    public virtual int Height => 40;

    [Required]
    public Vector2 Position { get; init; }

    [Required]
    public string Text { get; init; } 

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!this.Rect.IsEmpty)
        {
            spriteBatch.Draw(texture: this._texture, destinationRectangle: this.Rect, color: this.BackgroundColor);

            if (!string.IsNullOrEmpty(this.Text))
            {
                (float CenterX, float CenterY) textPos = this.GetTextCenterPos(text: this.Text);
                spriteBatch.DrawString(spriteFont: this._font, text: this.Text, position: new Vector2(x: textPos.CenterX, y: textPos.CenterY), color: this.TextColor);
            }
        }
    }

    public void Update()
    {
        MouseState mouseState = Mouse.GetState();
        Rectangle cursor = new Rectangle(x: mouseState.X, y: mouseState.Y, width: 1, height: 1);

        if (cursor.Intersects(value: this.Rect))
        {
            if (mouseState.LeftButton == ButtonState.Pressed && !this._hasClicked)
            {
                this._hasClicked = true;
                Singleton.MessageBus.Publish<string, string>(key: TextKey.IOKey, value: this.Text);
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                this._hasClicked = false;
            }
        }
    }

    private (float, float) GetTextCenterPos(string text)
    {
        float centerX = (this.Rect.X + (this.Rect.Width / 2)) - this._font.MeasureString(text: this.Text).X / 2;
        float centerY = (this.Rect.Y + (this.Rect.Height / 2)) - this._font.MeasureString(text: this.Text).Y / 2;

        return (centerX, centerY);
    }
}