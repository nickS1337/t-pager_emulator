using System;
using System.IO.Enumeration;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using SplashKitSDK;
namespace CustomProgram;

class NotesEditor : UIElement
{

    //The notes screen

    private String _FileName;
    private String _FilePath;

    private int _SelectedLine;
    private int _MaxLines;

    public NotesEditor() {

        //UITitle = "/HOME/";

        _FileName = "Untitled.txt";
        _FilePath = "/root/notes/" + _FileName + ".txt";

        //The line selected by the user:
        _SelectedLine = 0;

        _MaxLines = 999;

        // NewElement(UI_ELEMENT.UI_LISTMENU, "List");
        // ListMenu _ListMenu = Element<ListMenu>("List");
        
        // _ListMenu.AddItem("Music library", "SCREEN_MUSIC_LIBRARY");
        // _ListMenu.AddItem("New Notes editor", "SCREEN_NOTES_EDITOR");
        // _ListMenu.AddItem("Browse files", "SCREEN_FILE_SYSTEM");

    }

    public String FileName
    {
        get { return _FileName; }
        set { _FileName = FileName; }
    }

    public String FilePath
    {
        get { return _FilePath; }
        set { _FilePath = FileName; }
    }
    
    public override void DrawElement()
    {
        
    }


}