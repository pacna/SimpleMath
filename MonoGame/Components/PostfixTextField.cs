using Microsoft.Xna.Framework.Graphics;
using SimpleMath.MonoGame.Constants;
using SimpleMath.MonoGame.MessageBus;

namespace SimpleMath.MonoGame.Components;

public class PostfixTextField : BaseTextField
{
    public PostfixTextField(SpriteFont font): base(font: font)
    {
    }

    protected override string Id => TextKey.PostfixKey;

    public override void HandleMsg(IMessageDoc msg)
    {
        if (msg.Key as string != this.Id)
        {
            return;
        }

        IList<string> postfixNotation = msg.Value as string[];
        this.TextBuilder.Clear();

        if (postfixNotation.IsNullOrEmpty())
        {
            return;
        }
    
        this.TextBuilder.Append(string.Join("", postfixNotation));
    }

}