using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SimpleMath.MonoGame;
using SimpleMath.MonoGame.Components;
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
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        this._spriteBatch = new SpriteBatch(graphicsDevice: GraphicsDevice);
        Singleton.Calculator.LoadButtons(
            ButtonFactory.CreateClearButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Clear, position: new Vector2(x: 0 + BoxModel.BorderWidth, y: 148)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.LeftParenthesis, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 1) + BoxModel.BorderWidth, y: 148)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.RightParenthesis, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 2) + BoxModel.BorderWidth, y: 148)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Power, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 3) + BoxModel.BorderWidth, y: 148)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Seven, position: new Vector2(x: 0 + BoxModel.BorderWidth, y: 204)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Eight, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 1) + BoxModel.BorderWidth, y: 204)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Nine, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 2) + BoxModel.BorderWidth, y: 204)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Division, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 3) + BoxModel.BorderWidth, y: 204)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Four, position: new Vector2(x: 0 + BoxModel.BorderWidth, y: 260)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Five, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 1) + BoxModel.BorderWidth, y: 260)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Six, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 2) + BoxModel.BorderWidth, y: 260)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Multiplication, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 3) + BoxModel.BorderWidth, y: 260)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.One, position: new Vector2(x: 0 + BoxModel.BorderWidth, y: 316)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Two, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 1) + BoxModel.BorderWidth, y: 316)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Three, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 2) + BoxModel.BorderWidth, y: 316)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Subtraction, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 3) + BoxModel.BorderWidth, y: 316)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Zero, position: new Vector2(x: 0 + BoxModel.BorderWidth, y: 372)),
            ButtonFactory.CreateSubmitButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Submit, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 1) + BoxModel.BorderWidth, y: 372)),
            ButtonFactory.CreateBasicButton(graphicsDevice: GraphicsDevice, font: Content.Load<SpriteFont>("Fonts/Default"), text: CalculatorSymbol.Addition, position: new Vector2(x: ((60 + BoxModel.BorderWidth) * 3) + BoxModel.BorderWidth, y: 372))
        );

        Singleton.Calculator.LoadTextFields(
            io: new IOTextField(font: Content.Load<SpriteFont>("Fonts/TextField"))
            {
                Position = new Vector2(x: 16, y: 96)
            },
            postFix: new PostfixTextField(font: Content.Load<SpriteFont>("Fonts/TextField"))
            {
                Position = new Vector2(x: 16, y: 56)
            }
        );

        Singleton.MessageBus.AddHandlers(new [] { Singleton.Calculator.IO, Singleton.Calculator.PostFix });
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        Singleton.MessageBus.Run();
        Singleton.Calculator.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(CustomColor.PrimaryMineShaft);

        // TODO: Add your drawing code here

        base.Draw(gameTime);

        this._spriteBatch.Begin();
        Singleton.Calculator.Draw(spriteBatch: this._spriteBatch);
        this._spriteBatch.End();
    }
}
