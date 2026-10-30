using System;
using SplashKitSDK;
namespace CustomProgram;

class ControlOptions : UIElement
{

    //Possible keyboard options to show
    public List<ControlOption> options;

    private const int KeyPaddingX = 4;
    private const int KeyPaddingY = 0;
    private const int DescriptionSpacing = 5;
    private const int OptionSpacing = 15;

    public ControlOptions() {
        
        /*
         * k = Key
         * kd = KeyDescription
        */

        Width = SplashKit.CurrentWindowWidth();

        // Allow padding inside the key and around the control bar.
        Height = SplashKit.TextHeight("Ag", FontName, TextSize)
               + (KeyPaddingY * 2)
               + (TextPadding * 2);

        X = 0;
        Y = SplashKit.CurrentWindowHeight() - Height;

        options = [];

    }

    public void AddOption(String key, String value)
    {
        options.Add(new ControlOption(key, value));
    }

    public override void DrawElement()
    {
        // Redraw the background each frame.
        SplashKit.FillRectangle(Color.DarkGray, X, Y, Width, Height);

        double optionsIncrement = X + TextPadding;

        for (int i = 0; i < options.Count; i++)
        {
            string key = options[i].Key;
            string description = options[i].Value;

            // Measure the text using the same font and size used to draw it.
            int keyTextWidth = SplashKit.TextWidth(key, FontName, TextSize);
            int keyTextHeight = SplashKit.TextHeight(key, FontName, TextSize);

            int descriptionWidth =
                SplashKit.TextWidth(description, FontName, TextSize);
            int descriptionHeight =
                SplashKit.TextHeight(description, FontName, TextSize);

            // Add padding on both sides of the key text.
            int keyWidth = keyTextWidth + (KeyPaddingX * 2);
            int keyHeight = keyTextHeight + (KeyPaddingY * 2);

            // Centre the key vertically within the control bar.
            double keyY = Y + (Height - keyHeight) / 2.0;

            SplashKit.FillRectangle(
                Color.DarkBlue,
                optionsIncrement, keyY,
                keyWidth, keyHeight
            );

            // Move the text inside the key's padded area.
            SplashKit.DrawText(
                key, Color.White, FontName, TextSize,
                optionsIncrement + KeyPaddingX,
                keyY + KeyPaddingY
            );

            // Position the description after the key, with a small gap.
            double descriptionX =
                optionsIncrement + keyWidth + DescriptionSpacing;

            double descriptionY =
                Y + (Height - descriptionHeight) / 2.0;

            SplashKit.DrawText(
                description, Color.White, FontName, TextSize,
                descriptionX, descriptionY
            );

            // Start the next option after the complete key-description pair.
            optionsIncrement = descriptionX
                             + descriptionWidth
                             + OptionSpacing;
        }
    }
}