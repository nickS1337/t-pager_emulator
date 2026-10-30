using System;
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

}