using System;
using SplashKitSDK;
namespace CustomProgram;

class ScreenControl
{

    //The actual class to call when making a user interface

    private TitleTop _TitleTop;

    public ScreenControl() {
        
        //Set the background colour to black:
        SplashKit.ClearScreen(Color.Black);

        _TitleTop = new TitleTop("HOME");

    }

}