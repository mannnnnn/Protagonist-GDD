///drawInventoryWindow(startX, startY, border, width, height, cellWidth, cellHeight, totalWidth, totalHeight, inventory, selected, drawText, textBorder)
var startX = argument0;
var startY = argument1;
var border = argument2;
var width = argument3;
var height = argument4;
var cellWidth = argument5;
var cellHeight = argument6;
var totalWidth = argument7;
var totalHeight = argument8;
var descHeight = argument9;
var inventory = argument10;
var selected = argument11;
var drawText = argument12;
var textBorder = argument13;

var B = border * 0.5;

/// draw big window
drawInventoryRect(startX, startY, totalWidth, totalHeight, B);
/// draw subwindows
for (var i = 0; i < width; i++)
{
    var X = getNthCell(startX, border, cellWidth, i);
    for (var j = 0; j < height; j++)
    {
        var Y = getNthCell(startY, border, cellHeight, j);
        drawInventoryRect(X, Y, cellWidth, cellHeight, B);
        var index = XYToIndex(i, j, width);
        if (index == selected)
        {
            drawInventoryRectColor(X, Y, cellWidth, cellHeight, B, c_white, 0.3);
        }
        if (index >= 0 && index < ds_list_size(inventory))
        {
            var item = inventory[| index];
            draw_sprite_stretched(item[| ITEM_SPRITE], item[| ITEM_IMAGE], X + B, Y + B, cellWidth - B, cellHeight - B);
        }
    }
}

/// draw desc window
var X = getNthCell(startX, border, cellWidth, 0);
var Y = getNthCell(startY, border, cellHeight, height);
var descWidth = totalWidth - (border * 2); 

// image box/desc box horizontal separation
var imgdescSep = 2 * B;

/// draw item image box
drawInventoryRect(X, Y, descHeight, descHeight, B);

/// draw item desc box
drawInventoryRect(X + descHeight + imgdescSep, Y, descWidth - descHeight - imgdescSep, descHeight, B);

// draw selected item
if (selected >= 0 && selected < ds_list_size(inventory))
{
    // settings
    draw_set_color(c_white);
    draw_set_font(fnt_inventory);
    // draw item image
    var item = inventory[| selected];
    draw_sprite_stretched(item[| ITEM_SPRITE], item[| ITEM_IMAGE], X + B, Y + B, descHeight - B, descHeight - B);
    if (drawText)
    {
        draw_set_halign(fa_center);
        draw_set_valign(fa_top);
        draw_text_ext(X + (descHeight * 0.5) + B, Y + textBorder, item[| ITEM_NAME], -1, descHeight - (textBorder * 2));
        // draw item desc
        draw_set_halign(fa_left);
        draw_set_valign(fa_top);
        draw_text_ext(X + descHeight + imgdescSep + B + textBorder, Y + textBorder, item[| ITEM_DESC], -1, descWidth - descHeight - (2 * textBorder) - imgdescSep);
    }
}
