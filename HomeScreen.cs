using System;
using System.Runtime.Serialization;
using SplashKitSDK;
namespace CustomProgram;

class HomeScreen : Screen
{

    //The home screen user interface

    public HomeScreen() {

        UITitle = "/HOME/";

        NewElement(UI_ELEMENT.UI_LISTMENU, "List");
        ListMenu _ListMenu = Element<ListMenu>("List");
        
        _ListMenu.AddItem("Music library", "SCREEN_MUSIC_LIBRARY");
        _ListMenu.AddItem("New Notes editor", "SCREEN_NOTES_EDITOR");
        _ListMenu.AddItem("Browse files", "SCREEN_FILE_SYSTEM");

    }

    public override void KeyboardEvents(int k)
    {

        //Console.WriteLine("Received key: " + k);

        KeyCode key = (KeyCode)k;
        
        switch (key)
        {
            
            case KeyCode.WKey:
            {
                Element<ListMenu>("List").FocusPreviousElement();
                break;
            }

            case KeyCode.SKey:
            {
                Element<ListMenu>("List").FocusNextElement();
                break;
            }

            case KeyCode.ReturnKey:
            {
                //Element<ListMenu>("List").FocusNextElement();
                String selected = Element<ListMenu>("List").SelectedOption();
                //Console.WriteLine("Selected item: " + selected);
                // if (Enum.TryParse<SCREEN_STATE>(screenName, out SCREEN_STATE screen))
                // {
                //     ChangeScreen(screen);
                // }
                RequestScreen(selected);
                break;
            }

        }

    }

}