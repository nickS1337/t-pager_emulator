using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using SplashKitSDK;
namespace CustomProgram;

class MusicLibrary : Screen
{

    //private Table _Table;
    //The table being focused
    private String _FocusedTableID;

    public MusicLibrary()
    {

        UITitle = "/HOME/MusicLibrary/";
        Console.WriteLine("Screen toggled to " + UITitle);
    
        NewElement(UI_ELEMENT.UI_MUSICSCRUBBER, "MusicDetails");
        
        _FocusedTableID = "Playlists";

        // Element<Table>("MusicDetails").SetHeaders(
        //     new List<String>
        // );

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
        Console.WriteLine(_FocusedTableID + ", CheckEndOfTable(-1):" + CheckEndOfTable(-1));
        Console.WriteLine(_FocusedTableID + ", CheckEndOfTable(1):" + CheckEndOfTable(1));

        KeyCode key = (KeyCode)k;
        
        switch (key)
        {
            
            case KeyCode.WKey:
            {

                if (CheckEndOfTable(-1))
                {
                    ToggleFocusedTable();
                } else
                {
                    Element<Table>(_FocusedTableID).FocusPreviousElement();
                }

                break;
            }

            case KeyCode.SKey:
            {

                if (CheckEndOfTable(1))
                {
                    ToggleFocusedTable();
                } else
                {
                    Element<Table>(_FocusedTableID).FocusNextElement();
                }

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

        //Check if the current position of any of the tables was reset:
        // if (Element<Table>("Playlists").)
        // {
            
        // }

    }

    public bool CheckEndOfTable(int x)
    {

        //Check if increasing or decreasing the table by x will reset the position
        //i.e. from the start of the list to the end of the list
        //OR from the end of the list to the start of the list

        Table table = Element<Table>(_FocusedTableID);

        //Console.WriteLine("table.SelectedItem: "+table.SelectedItem + " + x:" +x +" = " + (table.SelectedItem + x));

        if (
            (table.SelectedItem + x) >= table.RowCount || 
            (table.SelectedItem + x) < 0
        )
        {
            //ToggleFocusedTable();
            return true;
        }

        return false;

    }

    public void ToggleFocusedTable()
    {
        
        //Invert the active table.
        //So if Playlists is active, the focused table will be Library
        //if Library, then Playlists is active

        if (_FocusedTableID == "Playlists")
        {
            Element<Table>("Playlists").Focused = false;
            Element<Table>("Library").Focused = true;
            _FocusedTableID = "Library";
        } else
        {
            Element<Table>("Playlists").Focused = true;
            Element<Table>("Library").Focused = false;  
            _FocusedTableID = "Playlists";
        }

    }

}