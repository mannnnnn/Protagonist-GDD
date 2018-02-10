///drawOptionsMenu(submenus, n, startX, startY, width, height, border, outsideBorder, textBorder, drawText, textalpha, alpha)
var submenus = argument0;
var n = argument1;
var startX = argument2;
var startY = argument3;
var width = argument4;
var height = argument5;
var border = argument6;
var outsideBorder = argument7;
var textBorder = argument8;
var drawText = argument9;
var textAlpha = argument10;
var alpha = argument11;

/// draw all options
for (var i = 0; i < n; i++)
{
    draw_set_alpha(1);
    var posY = getNthCell(startY, outsideBorder, height, i);
    // draw rectangle
    drawButtonRect(startX, posY, width - 6, height - 6, obj_buttons.button, c_white, c_white, alpha, alpha);
    if (drawText && ds_exists(submenus, ds_type_list))
    {
        draw_set_alpha(textAlpha);
        draw_set_font(fnt_dialogue);
        draw_set_halign(fa_left);
        draw_set_valign(fa_top);
        draw_set_color(c_white);
        // draw text
        var submenu = submenus[| i];
        draw_text(startX + textBorder + 10, posY + textBorder, submenu.name);
    }
}
