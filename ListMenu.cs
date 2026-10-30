using System;
using SplashKitSDK;
namespace CustomProgram;

class ListMenu : UIElement 
{

    private int _ListItemHeight;
    private int _ListItemWidth;

    private List<ControlOption> _Items;
    
    //SelectedItem is the currently selected/highlighted item by the user
    private int _SelectedItem;

    public ListMenu() 
    {

        //By default, the selected/highlighted item is the first item on the list.
        _SelectedItem = 0;

        _Items = [];

        _ListItemHeight = TextHeight + TextPadding;
        _ListItemWidth  = 432; //0.9 * 444;
        
        Title = "ListMenu";

        Console.WriteLine("_ListItemHeight: "+ _ListItemHeight +", TextPadding: " + TextPadding);

    }

    public void AddItem(String itemName, String itemDescription)
    {
        _Items.Add(new ControlOption(itemName, itemDescription));
        Height += + _ListItemHeight;
    }

    private void DrawItem(int itemIndex)
    {

        ControlOption Item = _Items[itemIndex];

        int ItemY = itemIndex * _ListItemHeight + Y;
        Color color     = Color.Black;
        Color textColor = Color.White; 

        if (itemIndex == _SelectedItem)
        {
            color     = Color.White;
            textColor = Color.Black;
        }

        //First draw a box around the FillRect
        SplashKit.FillRectangle(
            color,
            X,
            ItemY,
            _ListItemWidth,
            _ListItemHeight
        );

        // Console.WriteLine("_ListItemHeight: " + _ListItemHeight + ", TextHeight: " + _TextSize);
        
        //Then draw the text:
        SplashKit.DrawText(
            Item.Value,
            textColor, FontName, TextSize,
            X+TextPadding,
            ItemY+TextPadding/2
        );

    }

    public void FocusPreviousElement()
    {
        int nextItem = _SelectedItem + 1;
        if (nextItem < _Items.Count)
        {
            nextItem = _Items.Count;
        }
    }

    public void FocusNextElement()
    {
        int nextItem = _SelectedItem + 1;
        if (nextItem > _Items.Count)
        {
            nextItem = 0;
        }
    }

    public override void DrawElement() 
    {
        for (int i = 0; i < _Items.Count; i++)
        {
            DrawItem(i);
        }
    }

}