using System;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using SplashKitSDK;
namespace CustomProgram;

class Table : UIElement
{

    //Create tables
    //Use cases: browse files, view music library & playlists
    private List<String> _TableHeaders;

    //Row contents. List within list.
    private List<List<String>> _RowContents;

    //The maximum width of each row. This determines the column widths
    private List<int> _ColumnWidths;

    //The actual column widths. This is what will be used when actually drawing each row column:
    private List<int> _ActualColumnWidths;

    private int RowHeight;
    
    public Table()
    {

        _TableHeaders = [];
        _RowContents  = [];
        _ColumnWidths = [];

        _ActualColumnWidths = [];

        Width = 432;
        Title = "Table";
        
        RowHeight = TextHeight;

        //SetHeaders(headers);
        
    }

    public void SetHeaders(List<String> headers)
    {
        
        //Set the table headers only if:
        //There are no rows yet

        if (_RowContents.Count == 0)
        {
            _TableHeaders = headers;
            List <int> cw = [];
            for (int i = 0; i < headers.Count; i++)
            {
                cw.Add(SplashKit.TextWidth(headers[i], FontName, TextSize));
            }
            _ColumnWidths = cw;
        }
        
    }

    public void AddRow(List<String> row)
    {
        
        //Add rows to the table

        //Do not add if either conditions are met:
        //A. There are no table headers
        //B. Row length is not equal to the total number of table headers

        bool bool1 = row.Count == _TableHeaders.Count;
        bool bool2 = _TableHeaders.Count != 0;

        if (bool1 && bool2)
        {

            //Compare each row item textwidth to all items in _ColumnWidths:
            for (int i = 0; i < row.Count; i++)
            {
                
                if (_ColumnWidths.Count == 0)
                {
                    
                    _ColumnWidths[i] = SplashKit.TextWidth(row[i], FontName, TextSize);

                } else
                {
                    
                    if (_ColumnWidths[i] < row[i].Length)
                    {
                        _ColumnWidths[i] = row[i].Length;
                    }

                }

            }

            _RowContents.Add(row);
        
        } else
        {
            Console.WriteLine("[Table.cs] Could not add row to Table (row.Count: "+ row.Count+", _TableHeaders.Count: "+_TableHeaders.Count+", row.Count != _TableHeaders.Count: "+bool1+") ");
        }

    }

    public override void DrawElement()
    {

        //TODO HERE: split the drawing of the header and rows into seperate methods

        if (_TableHeaders.Count == 0)
        {
            return;
        }

        //For now, just perfectly divide the widths of all the columns into the number of total
        //headers there are
        int ColumnWidth = Width / _TableHeaders.Count;

        //First draw the table headers:
        SplashKit.FillRectangle(
            Color.DarkRed,
            X,
            Y,
            Width,
            RowHeight
        );

        //Then draw each individual table header text:
        for (int i = 0; i < _TableHeaders.Count; i++)
        {
            SplashKit.DrawText(_TableHeaders[i].ToUpper(), Color.White, FontName, TextSize, X + (i * ColumnWidth), Y);
        }

        
        //Then lastly, each individual row
        for (int i = 0; i < _RowContents.Count; i++)
        {
            List <String> row = _RowContents[i];
            int k = i + 1;
            for (int j = 0; j < row.Count; j++)
            {
                SplashKit.DrawText(
                    row[j],
                    Color.White,
                    FontName,
                    TextSize,
                    X + (j * ColumnWidth),
                    Y + ((i + 1) * RowHeight)
                );
            }

        }

    }

}