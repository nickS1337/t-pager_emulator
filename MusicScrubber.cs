using System;
using System.Runtime.Serialization;
using System.Security.Principal;
using SplashKitSDK;
namespace CustomProgram;

class MusicScrubber : UIElement 
{

    //Music scrubber
    //Display the scrubber UI element, the song name,
    //and the playlist (if applicable)
    
    //SelectedItem is the currently selected/highlighted item by the user

    private String _SongName;
    private String _SongAlbum;
    private String _SongArtist;

    //Total song duration (ms)
    private int _SongDuration;
    private String _SongPlaylist;
    private double _SongProgressPercent;
    
    //How much of the song have we played? (ms)
    private int _SongCurrentPosition;

    //Position of the scrubber (pixels)
    private int _ScrubberPositionX;
    private int _ScrubberPositionY;

    //Paused / playing
    private bool _Paused;
    private String _PlaybackStatus;

    public MusicScrubber()
    {
        
        //Height = TextHeight*5 + TextPadding;
        /* UI will look something like:

        NOW PLAYING:
        SONG NAME - ARTIST
        ----|-----------------------------
        1:23                          4:56
                         IN PLAYLIST: TEST

        */
        Width  = 432; //0.9 * 444;

        _Paused = true;

        _SongName = "Unknown";
        _SongAlbum = "Unknown Album";
        _SongArtist = "Unknown Artist";

        _SongPlaylist = "MAIN LIBRARY";

        _SongProgressPercent = 0.4;
        
    }


    public bool Paused
    {
        get { return _Paused; }
        set { _Paused = value; }
    }

    public String SongName
    {
        get { return _SongName; }
        set { _SongName = value; }
    }

    public String SongArtist
    {
        get { return _SongArtist; }
        set { _SongArtist = value; }
    }

    public override void DrawElement()
    {

        string playbackStatus = Paused ? "PLAYBACK PAUSED" : "NOW PLAYING";
        int elementY = Y;
        Height = TextHeight * 3;

        SplashKit.FillRectangle(
            Color.Wheat,
            X,
            Y,
            Width,
            Height
        );

        //Playback status text
        SplashKit.DrawText(
            playbackStatus + ":",
            Color.Black,
            FontName,
            TextSize,
            X+ TextPadding,
            elementY
        );

        //Playlist name
        int inPlaylist_X = X+Width-SplashKit.TextWidth(_SongPlaylist, FontName, TextSize)-TextPadding;
        SplashKit.DrawText(
            _SongPlaylist,
            Color.Gray,
            FontName,
            TextSize,
            inPlaylist_X,
            elementY
        );

        elementY += TextHeight;

        //Song name and artist
        SplashKit.DrawText(
            SongName + " - " + SongArtist,
            Color.Black,
            FontName,
            TextSize,
            X+ TextPadding,
            elementY
        );

        //Song duration
        //** TEMPORARY ONLY **
        int playlistName_X = X+Width-SplashKit.TextWidth("1:23/4:56", FontName, TextSize)-TextPadding;
        SplashKit.DrawText(
            "1:23/4:56",
            Color.Gray,
            FontName,
            TextSize,
            playlistName_X,
            elementY
        );

        elementY += TextHeight + TextPadding*2;

        //Draw the scrubbing line:
        SplashKit.FillRectangle(
            Color.Black,
            X,
            elementY,
            Width,
            2
        );

        //Show a the scrubber position:
        SplashKit.FillRectangle(
            Color.Black,
            X + ((Width - 3) * _SongProgressPercent),
            elementY + 1 - (TextHeight / 2.0),
            2,
            TextHeight
        );

        Height = elementY;

    }

}