class ListMenu {

  private:

    bool active = false;

    //Number of menu items
    int MenuItems = 0;

    //List of menu items
    String items[50] = {};

    //Event listener status
    bool eventsActive = false;

    //The menu item that will play
    //void nextMenu;

    //Item currently selected by the user
    int currentlySelected = 0;

    //Item previously selected by the user
    int previouslySelected = 0;

    //Starting position of the text in the list
    int text_positionX = 10;
    int text_positionY = 30;

    int menuBg = 0x9CD3;

    int boxWidth  = 310 - TDECK_PADDING;
    int boxHeight;

    //Starting position of the list
    int positionX = 10;
    int positionY = 30;

  public:

    //Add items to the list of MenuItems
    void addItems(String list[], int listItems, String title = "", String title2 = "") {

      tft.fillScreen(TFT_DARKGREY);
      tft.setTextColor(TFT_WHITE);

      for (int i = 0; i < listItems; i++) {
        items[i] = list[i];
        MenuItems += 1;
      }

      //If we're using a title, then we will change the starting Y position of the list:
      if (title != "") {
        tft.drawString(title, positionX, positionY);
        positionY += 10;
        text_positionY = positionY;
      }

      //Also check for an optional secondary title2
      if (title2 != "") {
        tft.drawString(title2, positionX, positionY);
        positionY += 20;
        text_positionY = positionY;
      }

      //Draw a box around the list:
      boxHeight = (MenuItems * 10) + (TDECK_PADDING * 2);
      Serial.println("Rectangle X: " + String(text_positionX - TDECK_PADDING) + ", Y: " + String(text_positionY - TDECK_PADDING));
      Serial.println("Rectangle W: " + String(boxWidth) + ", H: " + String(boxHeight));//
      tft.fillRect(text_positionX - TDECK_PADDING, text_positionY - TDECK_PADDING, boxWidth, boxHeight, menuBg);

      drawList(false, title);

    }

    //Draw the actual list on the screen:
    void drawList(bool hide, String title = "") {

      //If hide is true, we are hiding the menu instead
      if (hide) {
        tft.setTextColor(TFT_SILVER);
      } else {
        tft.setTextColor(0x9CD3);
      }

      //tft.drawRect(positionX-TDECK_PADDING,
      //             positionY-TDECK_PADDING,
      //             boxWidth,
      //             boxHeight,
      //             TFT_WHITE);

      //      Serial.println("Rectangle X: " + String(positionX-TDECK_PADDING) + ", Y: " + String(positionY-TDECK_PADDING));
      //      Serial.println("Rectangle W: " + String(boxWidth) + ", H: " + String(boxHeight));//

      //Draw the list:
      for (int i = 0; i < MenuItems; i++) {

        String item = items[i];

        //        String itemPrefix = "";
        //
        //        if ((i-1) == (MenuItems-1)){
        //          itemPrefix = "";
        //        } else if ((i < MenuItems-2) && (i > MenuItems-1)){
        //          itemPrefix = "";
        //        }

        if (i == currentlySelected && !hide) {
          tft.setTextColor(TFT_WHITE);
          item = "> [" + items[i] + "]";
        } else if (i == currentlySelected && hide) {
          tft.setTextColor(TFT_WHITE);
          item = "> [" + items[i] + "]";
        }

        tft.drawString(item, positionX, positionY + (i * 10));
        tft.setTextColor(TFT_SILVER);

      }

      tft.setTextColor(TFT_WHITE);

    }

    //TODO HERE: Update the selected item so that we don't redraw the whole menu each time
    void redrawList(int i, int j) {

      //Change the selected item i, to j (list item highlighting)

      //Draw a box around the list:
      int boxWidth  = 150;
      int boxHeight = (MenuItems * 10) + (TDECK_PADDING * 2);
      //      tft.fillRect(text_positionX-TDECK_PADDING, text_positionY-TDECK_PADDING, boxWidth, boxHeight, 0x9CD3); //THIS LINEy

      String currentItem = "> [" + items[i] + "]";
      String nextItem = "> [" + items[j] + "]";

      //Draw over the currently selected item:
      //      tft.setTextColor(TFT_SILVER);
      //      tft.drawString(currentItem, text_positionX, text_positionY + (i*10));
      //      tft.setTextColor(0x9CD3); //Then just redraw it normally
      //      tft.drawString(items[i], text_positionX, text_positionY + (i*10));
      //
      //      //Then draw for the next item:
      //      tft.setTextColor(TFT_WHITE);
      //      tft.drawString(items[j], text_positionX, text_positionY + (j*10));
      //      tft.setTextColor(0x9CD3); //Then just redraw it normally
      //      tft.drawString(nextItem, text_positionX, text_positionY + (j*10));

      int rowHeight = 10;
      int clearWidth = 280;

      int oldIndex = i;
      int newIndex = j;

      // clear old row
      tft.fillRect(text_positionX, text_positionY + oldIndex * rowHeight, clearWidth, rowHeight, menuBg); //menuBg 0x9CD3

      // draw old row normally
      tft.setTextColor(TFT_SILVER);
      tft.drawString(items[oldIndex], text_positionX, text_positionY + oldIndex * rowHeight);

      // clear new row
      tft.fillRect(text_positionX, text_positionY + newIndex * rowHeight, clearWidth, rowHeight, menuBg);

      // draw new selected row
      tft.setTextColor(TFT_WHITE);
      tft.drawString("> [" + items[newIndex] + "]", text_positionX, text_positionY + newIndex * rowHeight);

    }

    void updateSelected(int i) {

      //Update the selected item by i
      int nextItemIndex = currentlySelected + i;

      if (nextItemIndex < 0) {
        nextItemIndex = MenuItems - 1; //back of the list
      } else if (nextItemIndex >= MenuItems) {
        nextItemIndex = 0; //start of the list
      }

      //Serial.println(nextItemIndex);

      //Redraw the list
      redrawList(currentlySelected, nextItemIndex);
      //drawList(false); //false to hide it first
      currentlySelected = nextItemIndex; //then change the selected item
      //drawList(true); //then redraw the list

    }

    void updateKeyboardEvents(char keyValue) {

      //Serial.println("updateKeyboardEvents");

      if (keyValue != (char)0x00) {

        switch (keyValue) {

          //Case "w" (LATIN_SMALL_LETTER_W / 0x77)
          case 0x77:
            //Update the selected item:
            updateSelected(-1);
            break;


          //Case "s" (LATIN_SMALL_LETTER_S / 0x73);
          case 0x73:
            //Update the selected item:
            updateSelected(1);
            break;

          //Enter
          case 0x0D:

            break;

        };

      }
    }

    int getCurrentlySelectedMenuCode() {
      return currentlySelected;
    }

};
