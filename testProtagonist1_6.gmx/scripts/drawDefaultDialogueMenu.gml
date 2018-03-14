///drawDefaultDialogueMenu(optionsList, n, centerX, startY, width, height, border, outsideBorder, textBorder, drawText, textalpha)
var optionsList = argument0;
var n = argument1;
var centerX = argument2;
var startY = argument3;
var width = argument4;
var height = argument5;
var border = argument6;
var outsideBorder = argument7;
var textBorder = argument8;
var drawText = argument9;
var textAlpha = argument10;

/// draw all options
for (var i = 0; i < n; i++)
{
    draw_set_alpha(1);
    var posY = getNthCell(startY, outsideBorder, height, i);
    // draw rectangle
    drawDialogueRect(centerX - (width * 0.5), posY, width, height, border);
    if (drawText && ds_exists(optionsList, ds_type_list))
    {
        draw_set_alpha(textAlpha);
        draw_set_font(fnt_dialogue);
        draw_set_halign(fa_center);
        draw_set_valign(fa_top);
        draw_set_color(c_white);
        // draw text
        var option = optionsList[| i];
        draw_text_outlined(centerX, posY + textBorder, option[| MENU_OPTION_NAME]);
    }
}