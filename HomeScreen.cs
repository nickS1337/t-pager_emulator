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
        
        _ListMenu.AddItem("Music", "Music player");
        _ListMenu.AddItem("Notes", "New Notes editor");
        _ListMenu.AddItem("Files", "Browse files");

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
                int selected = Element<ListMenu>("List").SelectedOption();
                Console.WriteLine("Selected item: " + selected);
                RequestScreen(selected);
                break;
            }

        }

    }

}