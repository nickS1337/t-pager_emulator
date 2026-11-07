using System;
using SplashKitSDK;
namespace CustomProgram;

class FileSystemScreen : Screen
{

    //The file system screen
    //(display one directory at a time though)

    //This is SCREEN_FILE_SYSTEM

    private String _PreviousDirPath;
    private String _CurrentDirPath;
    private String _CurrentDirAbsolutePath;
    private Table _DirectoryOptions;

    //Full contents of the current file
    //Will use ControlOption here, with each Key being the name of the file, and Value being
    //the name of the type of each file (i.e. each file can either be a <DIR> or an actual file)
    private List<ControlOption> _CurrentDirContents;

    public FileSystemScreen()
    {

        _CurrentDirAbsolutePath = "/HOME/root/";
        UITitle = _CurrentDirAbsolutePath;

        NewElement(UI_ELEMENT.UI_TABLE, "Directory");

        Element<Table>("Directory").SetHeaders(
            new List<String> 
            {
               "Type",
               "Size",
               "Filename",
            }
        );

        _ControlOptions.AddOption("BKSP", "Back to " );

        _CurrentDirContents = [];

        _DirectoryOptions = Element<Table>("Directory");
        _DirectoryOptions.Focused = true;
    
        _CurrentDirPath = "root/";
        _PreviousDirPath = "";

        GetCurrentDirContents();

    }

    public void ToggleDirectory(String nextDir)
    {
        
        //Switch to a different directory;
        if (_PreviousDirPath != "" && 
            _PreviousDirPath != _CurrentDirPath)
        {
            UITitle = "HOME/" + _CurrentDirPath;
            _ControlOptions.RemoveOption("BKSP");
            _ControlOptions.AddOption("BKSP", "Back to " + _PreviousDirPath);

            GetCurrentDirContents();
        }

    }

    public void DirGoBack()
    {
        
        //Go back to the previous directory
        //Just switch _PreviousDirPath and _CurrentDirPath around
        String prev = _PreviousDirPath;
        String now = _CurrentDirPath;
        _PreviousDirPath = now;
        _CurrentDirPath  = prev;

        Console.WriteLine("Returning to " + _CurrentDirPath);

        ToggleDirectory(_CurrentDirPath);

    }

    public void GetCurrentDirContents()
    {

        _DirectoryOptions.EmptyRows();
        
        //Get the contents of the current directory
        //Use the System.IO file system to access the folder contents
        //https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo?view=net-10.0
        DirectoryInfo folder = new DirectoryInfo(_CurrentDirPath);
        FileSystemInfo[] files = folder.GetFileSystemInfos();

        for (int i = 0; i < files.Length; i++)
        {
            
            FileSystemInfo item = files[i];

            String fileName = item.Name;
            String fileType = (item.GetType().Name == "FileInfo") ? "" : "<DIR>";
            String fileSize = "--";

            if (fileType == "")
            {
                FileInfo file = (FileInfo)item;
                //Empty filetype signifies the item is a file
                //file.Length returns as bytes
                //https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo.length?view=net-10.0
                //with 2 significant figures here
                fileSize = $"{file.Length / (1024.0):F2} kB";
            } else
            {
                fileName = fileName + "/";
            }

            _DirectoryOptions.AddRow(new List<string>()
            {
                fileType,
                fileSize,
                fileName
            });

        }

        _DirectoryOptions.SelectedItem = 0;

    }

    public void CheckItemAction(List<String> item)
    {
        
        //Check what to do when an item is selected.
        //If the item is a <DIR>, then toggle the directory.
        //Otherwise, open the file if it contains a valid extension

        bool isDir = (item[1] == "<DIR>") ? true : false;
        String itemName = item[3];

        Console.WriteLine(itemName + ", DIR:"+isDir);

        if (isDir)
        {
            //Toggle the directory by the folder name (2nd option item in the list)
            _PreviousDirPath = _CurrentDirPath;
            _CurrentDirPath  = _PreviousDirPath + itemName;
            
            //String absolutePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //absolutePath.

            //_CurrentDirAbsolutePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Console.WriteLine(_CurrentDirPath);
            
            ToggleDirectory(itemName);
        } else
        {
            
        }

    }

    public override void KeyboardEvents(int k)
    {
        KeyCode key = (KeyCode)k;
        
        switch (key)
        {
            
            case KeyCode.WKey:
            {
                _DirectoryOptions.FocusPreviousElement();
                break;
            }

            case KeyCode.SKey:
            {
                _DirectoryOptions.FocusNextElement();
                break;
            }

            case KeyCode.BackspaceKey:
            {
                //Go back to the previous dir
                DirGoBack();
                break;
            }

            case KeyCode.ReturnKey:
            {
                // //Element<ListMenu>("List").FocusNextElement();
                // String selected = Element<ListMenu>("List").SelectedOption();
                // //Console.WriteLine("Selected item: " + selected);
                // // if (Enum.TryParse<SCREEN_STATE>(screenName, out SCREEN_STATE screen))
                // // {
                // //     ChangeScreen(screen);
                // // }
                // RequestScreen(selected);

                int selectedItem = _DirectoryOptions.SelectedItem;
                List<String> item = _DirectoryOptions.GetRow(selectedItem);

                //Action the item:
                CheckItemAction(item);

                break;
            }

        }

    }

}