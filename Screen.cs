using System;
using SplashKitSDK;
namespace CustomProgram;

abstract class Screen
{

    //The actual class to call when making a user interface

    //Screen will handle and track where to place new UI elements.
    //Through this, each individual screen will not need to individually position each UI element they might need.

    private String _UITitle;

    private TitleTop _TitleTop;
    public ControlOptions _ControlOptions;
    private List<UIElement> _UIElements;
    //private ListMenu _ListMenu;

    //Keep track of where new elements should be positioned,
    //starting at (aka the x and y offset for drawing elements):
    private int _ElementInitX = 20;
    private int _ElementInitY = 30;
    private Color _ScreenColor;

    //UI_ELEMENT is used to identify different types of UIs
    public enum UI_ELEMENT
    {
        UI_LISTMENU,
        UI_TABLE,
        UI_NOTES,
        UI_NOTES_TITLE,
        UI_NOTES_LINE,
        UI_CONTROLOPTIONS,
        UI_MUSICSCRUBBER,
        UI_ELEMENT_NOT_SET
    }


    public Screen(String title="UNKNOWN MENU") {

        _UITitle = title;

        _UIElements = [];

        _ScreenColor = Color.Black;
        
        //Set the background colour to black:
        SplashKit.ClearScreen(_ScreenColor);

        _TitleTop = new TitleTop(title);
        _ControlOptions = new ControlOptions();
        _ControlOptions.AddOption("W", "PREVIOUS");
        _ControlOptions.AddOption("S", "NEXT");

        // _ListMenu = new ListMenu();
        // _ListMenu.X = 20;
        // _ListMenu.Y = 30;
        // _ListMenu.AddItem("Music", "Music player");
        // _ListMenu.AddItem("Notes", "New Notes editor");
        // _ListMenu.AddItem("Files", "Browse files");

    }

    public void NewElement(UI_ELEMENT element, String element_id)
    {
        
        //Automatic positioning and stuff will go here
        //This can be further simplified ******************

        UIElement item;

        switch (element)
        {
            case UI_ELEMENT.UI_LISTMENU:
                item = new ListMenu();
                break;

            case UI_ELEMENT.UI_TABLE:
                item = new Table();
                break;

            case UI_ELEMENT.UI_MUSICSCRUBBER:
                item = new MusicScrubber();
                break;

            case UI_ELEMENT.UI_NOTES_TITLE:
                item = new NotesEditorTitle();
                break;

            case UI_ELEMENT.UI_NOTES_LINE:
                item = new NotesEditorLine();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(element));
        }

        item.ElementID = element_id;
        item.X = OffsetX;
        item.Y = OffsetY;

        _UIElements.Add(item);
        _ElementInitY += item.Height;

    }

    public Color ScreenColor
    {
        get { return _ScreenColor; }
        set { _ScreenColor = value; }
    }

    public String UITitle
    {
        get { return _UITitle; }
        set { 
            _TitleTop.Title = value;
            _UITitle = value; 
        }
    }

    public int OffsetX
    {
        get { return _ElementInitX; }
        set { _ElementInitX = value; }
    }

    public int OffsetY
    {
        get { return _ElementInitY; }
        set { _ElementInitY = value; }
    }

    //Finds an existing UI element by its ID and returns it as the requested type
    //T must be UIElement or a class that inherits from UIElement
    //i.e.: Element<ListMenu>("HomeMenu").AddItem("Music", "Music player");
    public T Element<T>(string elementId) where T : UIElement
    {
        //Search the dynamic elements for the matching ID.
        UIElement? element = _UIElements.Find(
            item => item.ElementID == elementId
        );

        if (element == null)
        {
            throw new ArgumentException(
                $"No UI element exists with ID '{elementId}'."
            );
        }

        //Check that the element is compatible with the requested type T
        //If it is, store its reference in typedElement and return it
        if (element is T typedElement)
        {
            return typedElement;
        }

        //The ID exists, but its element does not match the requested type
        throw new InvalidOperationException(
            $"UI element '{elementId}' is not a {typeof(T).Name}."
        );
    }

    public virtual void DrawElement()
    {

        _TitleTop.DrawElement();
        _ControlOptions.DrawElement();
        //_ListMenu.DrawElement();

        int elementX = OffsetX;
        int elementY = OffsetY;

        for (int i = 0; i < _UIElements.Count; i++)
        {

            //Console.WriteLine("Drawing " + _UIElements[i].ElementID + " at Y:" + _UIElements[i].Y);
            _UIElements[i].Y = elementY;
            _UIElements[i].DrawElement();

            elementY += _UIElements[i].Height;

        }

    }

    public abstract void KeyboardEvents(int key);

    //Event for when a screen change is requested
    public event Action<String>? ScreenRequested;

    protected void RequestScreen(String nextScreen)
    {
        // Invoke subscribers only if someone is listening.
        ScreenRequested?.Invoke(nextScreen);
    }

}