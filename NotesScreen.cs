using System;
using System.ComponentModel;
using System.Linq.Expressions;
using SplashKitSDK;
namespace CustomProgram;

class NotesScreen : Screen
{

    //The notes screen

    private NotesEditor _NotesEditor;
    private NotesEditorTitle _NotesEditorTitle;
    private FontInfo _FontInfo;

    private int _ContentWidth;
    private int _ContentHeight;
    
    public NotesScreen()
    {

        _FontInfo = new FontInfo();

        //Set a zero offset on the X axis, 
        //so each element in this screen can take the full width of the page
        OffsetX = 0;
        OffsetY = 0;//+ _FontInfo.TextPadding*2;
        
        _NotesEditor = new NotesEditor();
        //_NotesEditorTitle = new NotesEditorTitle();
        
        NewElement(UI_ELEMENT.UI_NOTES_TITLE, "NotesTitle");
        Element<NotesEditorTitle>("NotesTitle").TitleText = _NotesEditor.FilePath;

        NewElement(UI_ELEMENT.UI_NOTES_LINE, "NotesLine");
        Element<NotesEditorLine>("NotesLine").LineNumber  = "001";
        Element<NotesEditorLine>("NotesLine").LineContent = "Hello world";

        NewElement(UI_ELEMENT.UI_NOTES_LINE, "NotesLine1");
        Element<NotesEditorLine>("NotesLine1").LineNumber  = "002";
        Element<NotesEditorLine>("NotesLine1").LineContent = "Hello meow";

        //_NotesEditorTitle.TitleText = _NotesEditor.FilePath;

        UITitle = "/home/NotesEditor";

    }

    public override void KeyboardEvents(int key)
    {
        throw new NotImplementedException();
    }

}