using System;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using SplashKitSDK;
namespace CustomProgram;

class Table : UIElement
{

    //TODO: Automatically add row count 
    //(i.e. prepend "No." to the table headers, and prepend the row number to each row)

    //Create tables
    //Use cases: browse files, view music library & playlists
    private List<String> _TableHeaders;

    //Row contents. List within list.
    private List<List<String>> _RowContents;

    //The maximum width of each row. This determines the column widths
    private List<int> _ColumnWidths;

    //The actual column widths. This is what will be used when actually drawing each row column:
    private List<int> _ActualColumnWidths;
    private Color _HeaderColor;

    private int RowHeight;

    private bool _MenuFocused = false;

    //The item highlighted/selected by the user
    private int _SelectedItem;
    
    public Table()
    {

        _TableHeaders = [];
        _RowContents  = [];
        _ColumnWidths = [];

        _ActualColumnWidths = [];

        Width = 432;
        Title = "Table";
        
        RowHeight = TextHeight;

        _MenuFocused = false;
        _SelectedItem = 0;

        _HeaderColor = Color.MidnightBlue;

        //SetHeaders(headers);
        
    }

    public Color HeaderColor {
        get { return _HeaderColor; }
        set { _HeaderColor = value; }
    }

    public bool Focused
    {
        get { return _MenuFocused; }
        set { _MenuFocused = value; }
    }

    private void UpdateColumnWidths()
    {
        //Rules for this:
        /**
            A. Where possible, all columns except the last column
            must take up their preferred width.
            B. The last column must take up the remaining space.
            C. The last column must have a width no smaller than
            the textwidth of its header text, including padding.
        */

        _ActualColumnWidths.Clear();

        if (_TableHeaders.Count == 0)
        {
            return;
        }

        int lastColumn = _TableHeaders.Count - 1;

        //Reserve enough space for the final header
        int lastMinimumWidth =
            SplashKit.TextWidth(
                _TableHeaders[lastColumn].ToUpper(),
                FontName,
                TextSize
            ) + TextPadding * 2;

        if (lastMinimumWidth > Width)
        {
            throw new InvalidOperationException(
                "The final column header is wider than the table."
            );
        }

        int totalColumnWidths = 0;

        //Add the preferred widths of all columns except the last
        for (int i = 0; i < lastColumn; i++)
        {
            totalColumnWidths += _ColumnWidths[i];
        }

        //Shrink earlier columns only if necessary.
        int availableWidth = Width - lastMinimumWidth;

        double scale = 1.0;

        if (totalColumnWidths > availableWidth)
        {
            scale = (double)availableWidth / totalColumnWidths;
        }

        int usedWidth = 0;

        for (int i = 0; i < lastColumn; i++)
        {
            int columnWidth = (int)(_ColumnWidths[i] * scale);

            _ActualColumnWidths.Add(columnWidth);
            usedWidth += columnWidth;
        }

        //Final column fills the remaining space.
        _ActualColumnWidths.Add(Width - usedWidth);
    }

    public void SetHeaders(List<String> headers)
    {
        
        //Set the table headers only if:
        //There are no rows yet

        /*if (_RowContents.Count == 0)
        {
            
            _TableHeaders = ["No.", .. headers];

            //Temporary column widths
            List <int> cw = [];
            
            cw.Add(SplashKit.TextWidth("No.", FontName, TextSize));
            for (int i = 0; i < headers.Count; i++)
            {
                cw.Add(SplashKit.TextWidth(headers[i], FontName, TextSize) + TextPadding * 2);
            }

            //Set _ColumnWidths = temporary column widths
            _ColumnWidths = cw;
        }

        Height += RowHeight;*/

        if (_RowContents.Count == 0)
        {
            _TableHeaders = ["No.", .. headers];

            //Temporary column widths
            List<int> cw = [];

            for (int i = 0; i < _TableHeaders.Count; i++)
            {
                cw.Add(
                    SplashKit.TextWidth(
                        _TableHeaders[i].ToUpper(),
                        FontName,
                        TextSize
                    ) + TextPadding * 2
                );
            }

            //Set _ColumnWidths = temporary column widths
            _ColumnWidths = cw;

            UpdateColumnWidths();
            Height = RowHeight;
        }
        
    }

    public int SelectedItem
    {
        get { return _SelectedItem; }
        set { _SelectedItem = value; }
    }

    public int RowCount
    {
        get { return _RowContents.Count; }
    }

    public void AddRow(List<String> row)
    {
        
        //Add rows to the table

        //Do not add if either conditions are met:
        //A. There are no table headers
        //B. Row length is not equal to the total number of table headers

        //Insert the row count: 
        row.Insert(0, _RowContents.Count.ToString());
   
        bool bool1 = row.Count == _TableHeaders.Count;
        bool bool2 = _TableHeaders.Count != 0;


        if (bool1 && bool2)
        {

            //int totalColumnWidths = 0;

            //Compare each row item textwidth to all items in _ColumnWidths:
            for (int i = 0; i < row.Count; i++)
            {

                //Rules for this:
                /*
                A. Where possible, all columns except the last row must take up the prescribed with of each row
                B. The last column of each row must take up the remaining space of all other rows
                C. The last column of each row must have a width no smaller than the textwidth of the header text
                */ 

                int textWidth = SplashKit.TextWidth(row[i] + "A", FontName, TextSize);
                _ColumnWidths[i] = Math.Max(_ColumnWidths[i], textWidth);
                
                // if (_ColumnWidths.Count == 0)
                // {
                    
                //     _ColumnWidths[i] = textWidth;

                // } else
                // {
                    
                //     if (_ColumnWidths[i] < textWidth)
                //     {
                       
                //         // totalColumnWidths += totalColumnWidths;
                //         //_ColumnWidths[i] = SplashKit.TextWidth(row[i] + "A", FontName, TextSize);
                    
                //         //If this is the last iteration:
                //        /* if (i == row.Count - 1)
                //         {
                            
                //             _ColumnWidths[i] = Width - columnWidth;

                //         } else
                //         {
                //             _ColumnWidths[i] = columnWidth; //SplashKit.TextWidth(row[i] + "A", FontName, TextSize);
                //             totalColumnWidths += columnWidth;
                //         }*/

                //     }

                // }

            }

            _ActualColumnWidths.Clear();

            int lastColumn = _TableHeaders.Count - 1;

            //Reserve enough space for the final header
            int lastMinimumWidth =
                SplashKit.TextWidth(
                    _TableHeaders[lastColumn].ToUpper(),
                    FontName,
                    TextSize
                ) + TextPadding * 2;

            if (lastMinimumWidth > Width)
            {
                throw new InvalidOperationException(
                    "The final column header is wider than the table."
                );
            }

            int totalColumnWidths = 0;

            for (int i = 0; i < lastColumn; i++)
            {
                totalColumnWidths += _ColumnWidths[i];
            }

            //Shrink earlier columns only if necessary.
            int availableWidth = Width - lastMinimumWidth;

            double scale = totalColumnWidths > availableWidth
                ? (double)availableWidth / totalColumnWidths
                : 1.0;

            int usedWidth = 0;

            for (int i = 0; i < lastColumn; i++)
            {
                int columnWidth = (int)(_ColumnWidths[i] * scale);

                _ActualColumnWidths.Add(columnWidth);
                usedWidth += columnWidth;
            }

            // Final column fills the remaining space.
            _ActualColumnWidths.Add(Width - usedWidth);

            _RowContents.Add(row);
            Height += RowHeight;
        
        } else
        {
            Console.WriteLine("[Table.cs] Could not add row to Table (row.Count: "+ row.Count+", _TableHeaders.Count: "+_TableHeaders.Count+", row.Count != _TableHeaders.Count: "+bool1+") ");
        }

    }

    /* ============================================== */
    // THIS CAN BE INHERITED FROM ListMenu.cs
    public void FocusPreviousElement()
    {
        int nextItem = _SelectedItem - 1;
        //Console.WriteLine("PREV nextItem: " + nextItem + " _Items.Count: "+ _Items.Count);
        if (nextItem < 0)
        {
            nextItem = _RowContents.Count-1;
            //Console.WriteLine("nextItem<0: " + nextItem);
        }
        _SelectedItem = nextItem;
    }

    public void FocusNextElement()
    {
        int nextItem = _SelectedItem + 1;
        //Console.WriteLine("NEXT nextItem: " + nextItem + " _Items.Count: "+ _Items.Count);
        if (nextItem == _RowContents.Count || nextItem > _RowContents.Count)
        {
            nextItem = 0;
        }
        _SelectedItem = nextItem;
    }
    /* ============================================== */

    public override void DrawElement()
    {

        //TODO HERE: split the drawing of the header and rows into seperate methods
        

        if (_TableHeaders.Count == 0)
        {
            return;
        }

        //For now, just perfectly divide the widths of all the columns into the number of total
        //headers there are
        //int ColumnWidth = Width / _TableHeaders.Count;

        //First draw the table headers:
        SplashKit.FillRectangle(
            Color.MidnightBlue,
            X,
            Y,
            Width,
            RowHeight
        );

        //Then draw each individual table header text:
        int headerX = X;
        for (int i = 0; i < _TableHeaders.Count; i++)
        {

            SplashKit.DrawText(
                _TableHeaders[i].ToUpper(), 
                Color.White, 
                FontName, 
                TextSize, 
               // X + (i * ColumnWidth), 
               headerX + TextPadding,
               Y
            );  

            headerX += _ActualColumnWidths[i];

        }
        
        //Then lastly, each individual row
        for (int i = 0; i < _RowContents.Count; i++)
        {

            List <String> row = _RowContents[i];

            //The focused item is _SelectedItem + 1, as we need to skip the table headers (row 0)
            //int focusedItem = _SelectedItem + 1;

            Color rowTextColor = (_SelectedItem == i && Focused) ? Color.Black : Color.White;
            Color rowBgColor = (_SelectedItem == i && Focused) ? Color.Wheat : Color.Black;

            int rowY = Y + ((i + 1) * RowHeight);

            SplashKit.FillRectangle(
                rowBgColor,
                X,
                rowY,
                Width,
                RowHeight
            );

            //Start drawing each row at the table's left edge
            int cellX = X;

            for (int j = 0; j < row.Count; j++)
            {
                SplashKit.DrawText(
                    row[j],
                    rowTextColor,
                    FontName,
                    TextSize,
                    cellX + TextPadding,
                    rowY
                );

                //Move to the next column
                cellX += _ActualColumnWidths[j];
            }

        }

    }

}