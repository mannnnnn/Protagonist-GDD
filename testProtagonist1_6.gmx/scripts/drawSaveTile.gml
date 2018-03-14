///drawSaveTile(saveID, posX, posY, saveW, saveH, border, picX, picY, picW, picH, descX, descY, descW, descH, textBorder)
var saveID = argument0;
var posX = argument1;
var posY = argument2;
var saveW = argument3;
var saveH = argument4;
var border = argument5;
var picX = argument6;
var picY = argument7;
var picW = argument8;
var picH = argument9;
var descX = argument10;
var descY = argument11;
var descW = argument12;
var descH = argument13;
var textBorder = argument14;
var selected = argument15;
var alpha = 0;
if (selected)
{
    alpha = 0.3;
}

draw_set_font(fnt_save);

// draw the image
// if the save is there
// draw the save image
if (checkSavePath(saveID))
{
    // load in the image
    if (spritetable[| saveID] == noone)
    {
        spritetable[| saveID] = sprite_add(getSavePath(saveID, getSaveImageFilename()), 1, false, false, 0, 0);
    }
    var spr = spritetable[| saveID];
    // draw it
    drawButtonRect(picX, picY, picW - 6, picH - 6, obj_buttons.button, c_white, c_white, 1, 0);
    draw_sprite_stretched(spr, 0, picX + border + 1, picY + border + 1, picW - (2 * border) - 1, picH - (2 * border) - 1);
    drawButtonRect(picX, picY, picW - 6, picH - 6, obj_buttons.button, c_white, c_white, 0, 1);
}
// draw that there is no save
else
{
    drawButtonRectHighlight(picX, picY, picW - 6, picH - 6, obj_buttons.button, c_white, c_white, 1, 1, c_white, alpha);
    draw_set_halign(fa_center);
    draw_set_valign(fa_center);
    draw_set_color(c_white);
    draw_text_outlined(picX + (0.5 * picW), picY + (0.5 * picH), "No Save"); 
}

// draw the desc box
drawButtonRectHighlight(descX, descY, descW - 6, descH - 6, obj_buttons.button, c_white, c_white, 1, 1, c_white, alpha);
draw_set_halign(fa_left);
draw_set_valign(fa_top);
draw_set_color(c_white);
draw_text_outlined(descX + border + textBorder, descY + border + textBorder, "Save " + string(saveID));