using System;
using System.Data;
using System.Drawing;
using SplashKitSDK;
namespace CustomProgram;

abstract class UIElement {
    
    private String _Title;
    private String _ElementID;
    private int _X;
    private int _Y;
    private int _Height;
    private int _Width;
    private int _TextPadding;
    private int _TextSize;
    public readonly String FontName;
    public readonly int TextHeight;

    public UIElement() 
    {
        _ElementID = "NONE";
        FontName = "Datatype";
        _Title = "UNKNOWN UI ELEMENT";
        _TextPadding = 2;
        _TextSize = 12;
        TextHeight = SplashKit.TextHeight("A", FontName, TextSize);
        // Console.WriteLine("Height: " + Height + ", TextHeight: " + TextHeight);
    }

    public String Title {
        get { return _Title; }
        set { _Title = value; }
    }
    public String ElementID {
        get { return _ElementID; }
        set { _ElementID = value; }
    }

    public int X {
        get { return _X; }
        set { _X = value; }
    }

    public int Y {
        get { return _Y; }
        set { _Y = value; }
    }

    public int Height {
        get { return _Height; }
        set { _Height = value; }
    }

    public int Width {
        get { return _Width; }
        set { _Width = value; }
    }

    public int TextPadding {
        get { return _TextPadding; }
        set { _TextPadding = value; }
    }
    
    public int TextSize {
        get { return _TextSize; }
        set { _TextSize = value; }
    }

    public abstract void DrawElement();

}