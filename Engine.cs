using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleMath.MonoGame;
using SimpleMath.MonoGame.Constants;

namespace SimpleMath;

public class Engine : Game, IRun
{
    private readonly GraphicsDeviceManager _graphics;

    private SpriteBatch _spriteBatch;

    public Engine()
    {
        this._graphics = new GraphicsDeviceManager(this);
        this._graphics.PreferredBackBufferHeight = 428;
        this._graphics.PreferredBackBufferWidth = 320;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Window.Title = "Simple Math";
        base.Initialize();
    }

    protected override void LoadContent()
    {
        this._spriteBatch = new SpriteBatch(graphicsDevice: GraphicsDevice);

        Singleton.Calculator.LoadButtons(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"));
        Singleton.Calculator.LoadTextFields(font: Content.Load<SpriteFont>("Fonts/TextField"));

        Singleton.MessageBus.AddHandlers(new [] { Singleton.Calculator.IO, Singleton.Calculator.Postfix });
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        Singleton.MessageBus.Run();
        Singleton.Calculator.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(CustomColor.PrimaryMineShaft);

        base.Draw(gameTime);

        this._spriteBatch.Begin();
        Singleton.Calculator.Draw(spriteBatch: this._spriteBatch);
        this._spriteBatch.End();
    }
}
