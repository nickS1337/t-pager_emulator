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
        SCREEN_NOTES_EDITOR,
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
        SCREEN_STATE ActiveScreen = SCREEN_STATE.SCREEN_HOME;
        // Screen_v1 CurrentScreen = new Screen_v1();

        SplashKit.RegisterCallbackOnKeyTyped(CurrentScreen.KeyboardEvents);

        // void ChangeScreen(String requestedScreen)
        // {

        //     Console.WriteLine("[Program.cs] Screen change requested: " + requestedScreen);

        //     //SCREEN_STATE requestedScreen = (SCREEN_STATE)req;

        //     if (Enum.TryParse<SCREEN_STATE>(requestedScreen, out SCREEN_STATE screen))
        //     {
        //         ChangeScreen(screen);
        //     }
        //     else
        //     {
        //         Console.WriteLine("[Program.cs] Unknown screen enum: " + requestedScreen);
        //     }

        //     switch (requestedScreen)
        //     {
        //         case SCREEN_STATE.SCREEN_MUSIC_LIBRARY:
        //             CurrentScreen = new MusicLibrary();
        //         break;
        //     }

        // }

        void ChangeScreen(string requestedScreen)
        {
            Console.WriteLine(
                "[Program.cs] Screen change requested: " + requestedScreen
            );

            // Convert the string into a valid screen state.
            if (!Enum.TryParse<SCREEN_STATE>(requestedScreen, out SCREEN_STATE screen)
                || !Enum.IsDefined(typeof(SCREEN_STATE), screen))
            {
                Console.WriteLine(
                    "[Program.cs] Unknown screen enum: " + requestedScreen
                );

                return;
            }

            Screen nextScreen;

            switch (screen)
            {
                case SCREEN_STATE.SCREEN_MUSIC_LIBRARY:
                    nextScreen = new MusicLibrary();
                    break;

                case SCREEN_STATE.SCREEN_HOME:
                    nextScreen = new HomeScreen();
                    break;

                default:
                    Console.WriteLine("[Program.cs] Screen not implemented: " + screen);
                    return;
            }

            //Unsubscribe from the old screen before replacing it
            CurrentScreen.ScreenRequested -= ChangeScreen;

            CurrentScreen = nextScreen;
            ActiveScreen = screen;

            //Subscribe to navigation requests from the new screen
            CurrentScreen.ScreenRequested += ChangeScreen;
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
