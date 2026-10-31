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

    }

    public override void KeyboardEvents(int key)
    {
        // throw new NotImplementedException();
    }

}