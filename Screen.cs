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
    private ControlOptions _ControlOptions;
    private List<UIElement> _UIElements;
    //private ListMenu _ListMenu;

    //Keep track of where new elements should be positioned,
    //starting at:
    private int _ElementInitX = 20;
    private int _ElementInitY = 30;

    //UI_ELEMENT is used to identify different types of UIs
    public enum UI_ELEMENT
    {
        UI_LISTMENU,
        UI_TABLE,
        UI_NOTES,
        UI_CONTROLOPTIONS,
        UI_ELEMENT_NOT_SET
    }


    public Screen(String title="UNKNOWN MENU") {

        _UITitle = title;

        _UIElements = [];
        
        //Set the background colour to black:
        SplashKit.ClearScreen(Color.Black);

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

        UIElement item;

        switch (element)
        {
            
            case UI_ELEMENT.UI_LISTMENU:
            {

                //Console.WriteLine("Requested new UI_LISTMENU in Screen " + UITitle);
                item = new ListMenu();
                item.ElementID = element_id;
                item.X = _ElementInitX;
                item.Y = _ElementInitY;
                _UIElements.Add(item);
                _ElementInitY += item.Height;
                break;
            }

            case UI_ELEMENT.UI_TABLE:
            {
                item = new Table();
                item.ElementID = element_id;
                item.X = _ElementInitX;
                item.Y = _ElementInitY;
                _UIElements.Add(item);
                _ElementInitY += item.Height;
                break;
            }

        }

    }

    public String UITitle
    {
        get { return _UITitle; }
        set { 
            _TitleTop.Title = value;
            _UITitle = value; 
        }
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

        for (int i = 0; i < _UIElements.Count; i++)
        {
            _UIElements[i].DrawElement();
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