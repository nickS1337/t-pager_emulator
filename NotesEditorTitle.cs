using System;
using System.IO.Enumeration;
using System.Runtime.Serialization;
using Microsoft.VisualBasic.FileIO;
using SplashKitSDK;
namespace CustomProgram;

class NotesEditorTitle : UIElement
{

    //The notes screen title element:
    //Show the name of the file, but also the current mode the user is engaged with the file

    //This is UI_NOTES_TITLE

    private String _TitleText;

    //The colour of the title element will change depending on which editor mode is active
    private Color _ActiveColor;

    //The editing mode can be either:
    private enum MODES {
        EDIT,
        VIEW,
        SAVED,
        UNDO
    };

    private MODES _CurrentMode;

    private int _SelectedLine;

    public NotesEditorTitle() {

        //UITitle = "/HOME/";

        _TitleText = "Untitled";

        //The line selected by the user:
        _SelectedLine = 0;
        _CurrentMode = MODES.EDIT;

        Height = TextHeight + TextPadding*2;
        Width = SplashKit.CurrentWindowWidth();

        _ActiveColor = Color.CornflowerBlue;

        // NewElement(UI_ELEMENT.UI_LISTMENU, "List");
        // ListMenu _ListMenu = Element<ListMenu>("List");
        
        // _ListMenu.AddItem("Music library", "SCREEN_MUSIC_LIBRARY");
        // _ListMenu.AddItem("New Notes editor", "SCREEN_NOTES_EDITOR");
        // _ListMenu.AddItem("Browse files", "SCREEN_FILE_SYSTEM");

    }

    public String TitleText
    {
        get { return _TitleText; }
        set { _TitleText = value; }
    }

    private Color ActiveColor
    {
        get { return _ActiveColor; }
        set { _ActiveColor = value; }
    }
    
    public override void DrawElement()
    {
        
        //Draw a rectangle, then show the current editing mode on the right hand side of the screen
        SplashKit.FillRectangle(
            ActiveColor, 
            X,
            Y,
            Width,
            Height
        );

        //Draw the name of the file:
        SplashKit.DrawText(
            _TitleText,
            Color.White,
            FontName,
            TextSize,
            X + TextPadding,
            Y + TextPadding
        );

        //Then draw the currrent mode:
        String currentMode = _CurrentMode.ToString();
        int currentMode_width = SplashKit.TextWidth(
            currentMode,
            FontName,
            TextSize
        );
        // SplashKit.SetFontStyle(FontName, FontStyle.BoldFont);
        SplashKit.DrawText(
            currentMode,
            Color.White,
            FontName,
            TextSize,
            Width - currentMode_width - TextPadding*2,
            Y + TextPadding
        );
        // SplashKit.SetFontStyle(FontName, FontStyle.NormalFont);

    }


}