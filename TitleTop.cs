using System;
using SplashKitSDK;
namespace CustomProgram;

class TitleTop : UIElement
{
    
    public TitleTop(String _title)
    {
        
        Title = _title;
        X = 0;
        Y = 0;
        
        Width = SplashKit.CurrentWindowWidth();
        Height = SplashKit.TextHeight(Title, FontName, TextSize);
        
        Console.WriteLine("TitleTop (UI_TITLETOP) Height: " + Height + ", TextHeight: " + TextHeight + " X: " + X +", Y: "+ Y);

    }

    public override void DrawElement() {

        SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height+(TextPadding*2));
        SplashKit.DrawText(
            Title, 
            Color.White, 
            FontName, 
            TextSize, 
            TextPadding, 
            TextPadding
        );

        //Get the time details
        DateTime time = DateTime.Now;
        String timeString = time.ToString("hh:mm:ss tt").ToUpper();
        int timeStringWidth = SplashKit.TextWidth(timeString, FontName, TextSize);

        SplashKit.DrawText(timeString, Color.White, FontName, TextSize, (Width-TextPadding)-timeStringWidth, TextPadding);

    }

    

}