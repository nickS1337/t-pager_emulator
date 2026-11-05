using System;
using System.IO.Enumeration;
using System.Runtime.Serialization;
using Microsoft.VisualBasic.FileIO;
using SplashKitSDK;
namespace CustomProgram;

class NotesEditorLine : UIElement
{

    //Draw an individual line for the notes editor
    //Should look something like:
    //000 | Hello World
    private String _LineNumber;
    private String _LineContent;

    private bool _LineSelected;
    private int _LineBoxWidth;
    private int _LineContentWidth;
    private int _Padding;

    public NotesEditorLine() {

        //UITitle = "/HOME/";

        _LineNumber  = "????";
        _LineContent = "undefined"; 

        _Padding = TextPadding/2;

        Width  = SplashKit.CurrentWindowWidth();
        Height = TextHeight + _Padding*2;

        _LineBoxWidth     = SplashKit.TextWidth("AAAA", FontName, TextSize);
        _LineContentWidth =  Width - _LineBoxWidth;

        //Is this line selected by the user?
        //If yes, we will later change the colours of the line and text box
        _LineSelected = false;


    }

    public String LineNumber
    {
        get { return _LineNumber; }
        set { _LineNumber = value + " "; }
    }

    public String LineContent
    {
        get { return _LineContent; }
        set { _LineContent = value; }
    }

    public bool LineSelected
    {
        get { return _LineSelected; }
        set { _LineSelected = value; }
    }

    private void DrawLineBox()
    {
        
        SplashKit.FillRectangle(
            Color.DarkSlateGray,
            X,
            Y,
            _LineBoxWidth,
            Height
        );

        SplashKit.DrawText(
            _LineNumber,
            Color.White,
            FontName,
            TextSize,
            X + _Padding,
            Y + _Padding
        );

    }

    private void DrawTextContent()
    {
        
        SplashKit.FillRectangle(
            SplashKit.RGBColor(85, 101, 109),
            X + _LineBoxWidth,
            Y,
            _LineContentWidth,
            Height
        );

        SplashKit.DrawText(
            _LineContent,
            Color.White,
            FontName,
            TextSize,
            X + _LineBoxWidth + _Padding,
            Y + _Padding
        );

    }
    
    public override void DrawElement()
    {
        DrawLineBox();
        DrawTextContent();
    }


}