using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Components;
using SimpleMath.MonoGame.Components.Buttons;
using SimpleMath.MonoGame.Constants;

namespace SimpleMath.MonoGame;

public class GUICalculator
{
    private IEnumerable<IComponent> _buttons;

    private GUICalculator()
    {
    }

    public BaseTextField IO;
    public BaseTextField Postfix;

    public void LoadButtons(GraphicsDevice graphicsDevice, SpriteFont font)
    {
        float startingXPos = BoxModel.BorderWidth;
        float startingYPos = 148;

        List<BaseButton> buttons = new();

        string[][] buttonSymbols = new[]
        {
            new [] { CalculatorSymbol.Clear, CalculatorSymbol.LeftParenthesis, CalculatorSymbol.RightParenthesis, CalculatorSymbol.Power },
            new [] { CalculatorSymbol.Seven, CalculatorSymbol.Eight, CalculatorSymbol.Nine, CalculatorSymbol.Division },
            new [] { CalculatorSymbol.Four, CalculatorSymbol.Five, CalculatorSymbol.Six, CalculatorSymbol.Multiplication },
            new [] { CalculatorSymbol.One, CalculatorSymbol.Two, CalculatorSymbol.Three, CalculatorSymbol.Subtraction },
            new [] { CalculatorSymbol.Zero, CalculatorSymbol.Submit, CalculatorSymbol.Addition }
        };

        foreach (string[] row in buttonSymbols)
        {
            for (int i = 0; i < row.Length; i++)
            {
                BaseButton createdButton;

                if (row[i] == CalculatorSymbol.Clear)
                {
                    createdButton = ButtonFactory.CreateClearButton(graphicsDevice: graphicsDevice, font: font, text: row[i], position: new Vector2(x: startingXPos, y: startingYPos));
                }
                else if (row[i] == CalculatorSymbol.Submit)
                {
                    createdButton = ButtonFactory.CreateSubmitButton(graphicsDevice: graphicsDevice, font: font, text: row[i], position: new Vector2(x: startingXPos, y: startingYPos));
                }
                else
                {
                    createdButton = ButtonFactory.CreateBasicButton(graphicsDevice: graphicsDevice, font: font, text: row[i], position: new Vector2(x: startingXPos, y: startingYPos));
                }

                buttons.Add(createdButton);

                startingXPos += createdButton.Width + BoxModel.BorderWidth;

                if (i == row.Length - 1)
                {
                    startingXPos = BoxModel.BorderWidth;
                    startingYPos += createdButton.Height + BoxModel.BorderWidth;
                }
            }
        }

        this._buttons = buttons;
    }

    public void LoadTextFields(SpriteFont font)
    {
        this.IO = new IOTextField(font: font)
        {
            Position = new Vector2(x: 16, y: 96)
        };

        this.Postfix = new PostfixTextField(font: font)
        {
            Position = new Vector2(x: 16, y: 56)
        };
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach(IComponent button in this._buttons)
        {
            button.Draw(spriteBatch: spriteBatch);
        }

        this.IO.Draw(spriteBatch: spriteBatch);
        this.Postfix.Draw(spriteBatch: spriteBatch);
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