using System;
using System.Drawing;
using SplashKitSDK;
namespace CustomProgram;

class Program
{

    //List of enums to identify different screen states
    public enum SCREEN_STATE 
    {
        SCREEN_HOME,
        SCREEN_FILE_SYSTEM,
        SCREEN_NOTES,
        SCREEN_MUSIC_LIBRARY,
        SCREEN_MUSIC_PLAYLIST
    };

    static void Main(string[] args)
    {

        Window canvas_window;
        canvas_window = new Window("T-Pager Emulator", 480, 222);

        //Load the custom font
        SplashKit.LoadFont("Datatype", "fonts/Datatype.ttf");

        Screen CurrentScreen = new HomeScreen();
        // Screen_v1 CurrentScreen = new Screen_v1();

        SplashKit.RegisterCallbackOnKeyTyped(CurrentScreen.KeyboardEvents);

        void ChangeScreen(int req)
        {

            Console.WriteLine("Screen change requested: " + req);

            SCREEN_STATE requestedScreen = (SCREEN_STATE)req;

            switch (requestedScreen)
            {
                case SCREEN_STATE.SCREEN_MUSIC_LIBRARY:
                break;
            }

        }

        //Subscribe to screen change requests
        CurrentScreen.ScreenRequested += ChangeScreen;

        do
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(SplashKitSDK.Color.Black);

            CurrentScreen.DrawElement();

            SplashKit.RefreshScreen();

        } while (!canvas_window.CloseRequested);
    
    }
}
