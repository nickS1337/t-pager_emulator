using System;
using System.Data;
using System.Drawing;
using SplashKitSDK;
namespace CustomProgram;


public class FontInfo
{
    public string FontName { get; } = "Datatype";

    public int TextPadding { get; set; } = 2;
    public int TextSize { get; set; } = 12;

    public int TextHeight
    {
        get
        {
            return SplashKit.TextHeight("A", FontName, TextSize);
        }
    }
}