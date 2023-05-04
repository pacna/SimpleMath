using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Constants;
using SimpleMath.MonoGame.MessageBus;
using SimpleMath.PostfixMath;

namespace SimpleMath.MonoGame.Components;

public class IOTextField : BaseTextField
{
    public IOTextField(SpriteFont font): base(font: font)
    {
    }

    protected override string Id => TextKey.IOKey;

    public override void HandleMsg(IMessageDoc msg)
    {
        if (msg.Key as string != this.Id)
        {
            return;
        }

        string calculatorSymbol = msg.Value as string;
        if (calculatorSymbol != CalculatorSymbol.Clear && calculatorSymbol as string != CalculatorSymbol.Submit)
        {

            this.TextBuilder.Append(calculatorSymbol);
            return;
        }

        if (calculatorSymbol == CalculatorSymbol.Clear)
        {
            Singleton.MessageBus.Publish<string, string[]>(key: TextKey.PostfixKey, value: null);
            this.TextBuilder.Clear();
            return;
        }

        if (calculatorSymbol == CalculatorSymbol.Submit)
        {
            ShuntingYard shuntingYard = new ShuntingYard(this.TextBuilder.ToString());
            Singleton.MessageBus.Publish<string, string[]>(key: TextKey.PostfixKey, value: shuntingYard.ToPostfix());
            this.TextBuilder.Clear();
            this.TextBuilder.Append(shuntingYard.Evaluate());
        }
    }

}