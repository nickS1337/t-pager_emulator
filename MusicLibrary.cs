using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SplashKitSDK;
namespace CustomProgram;

class MusicLibrary : Screen
{

    //private Table _Table;

    public MusicLibrary()
    {

        UITitle = "/HOME/MusicLibrary/";
        Console.WriteLine("Screen toggled to " + UITitle);
    
        NewElement(UI_ELEMENT.UI_TABLE, "Playlists");

        Element<Table>("Playlists").SetHeaders(
            new List<String> 
            {
               "Playlist",
               "Tracks"
            }
        );

        Element<Table>("Playlists").AddRow(
            new List<String>
            {
                "Test 1",
                "12 tracks"
            }
        );
        Element<Table>("Playlists").AddRow(
            new List<String>
            {
                "Meow playlist",
                "12 tracks"
            }
        );

        NewElement(UI_ELEMENT.UI_TABLE, "Library");

        Element<Table>("Library").SetHeaders(
            new List<String> 
            {
               "Artist",
               "Track",
               "Album",
               "Year",
               "Duration"
            }
        );
        Element<Table>("Library").AddRow(
            new List<String>
            {
                "Soichi Terada",
                "Double Spire",
                "Asakusa Light",
                "2021",
                "4:30"
            }
        );
        Element<Table>("Library").AddRow(
            new List<String>
            {
                "Soichi Terada",
                "Silent Chord",
                "Asakusa Light",
                "2021",
                "4:50"
            }
        );

        Element<Table>("Playlists").Focused = true;

    }

    public override void KeyboardEvents(int k)
    {

        Console.WriteLine("Received key: " + k);

        KeyCode key = (KeyCode)k;
        
        switch (key)
        {
            
            case KeyCode.WKey:
            {
                Element<Table>("Playlists").FocusPreviousElement();
                break;
            }

            case KeyCode.SKey:
            {
                Element<Table>("Playlists").FocusNextElement();
                break;
            }

            case KeyCode.ReturnKey:
            {
                // //Element<ListMenu>("List").FocusNextElement();
                // String selected = Element<ListMenu>("List").SelectedOption();
                // //Console.WriteLine("Selected item: " + selected);
                // // if (Enum.TryParse<SCREEN_STATE>(screenName, out SCREEN_STATE screen))
                // // {
                // //     ChangeScreen(screen);
                // // }
                // RequestScreen(selected);
                break;
            }

        }

    }

}