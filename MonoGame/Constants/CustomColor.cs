using Microsoft.Xna.Framework;

namespace SimpleMath.MonoGame.Constants;

public static class CustomColor
{
    public static Color PrimaryMineShaft = new(r: 37, g: 37, b: 37, alpha: 255); // should be used for the background
    public static Color SecondaryMineShaft = new(r: 55, g: 55, b: 55, alpha: 255); // button background color
    public static Color Tamarillo = new(r: 162, g: 18, b: 35, alpha: 255); // for the delete button
    public static Color SanFelix = new(r: 13, g: 118, b: 29, alpha: 255); // for the equal button
    public static Color White = Color.White; // text color
    public static Color Black = Color.Black; // border color
}