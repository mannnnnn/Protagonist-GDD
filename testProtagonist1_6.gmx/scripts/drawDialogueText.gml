///drawDialogueText(startX, startY, border, width, height, text, font, textBorder)
var startX = argument0;
var startY = argument1;
var border = argument2;
var width = argument3;
var height = argument4;
var text = argument5;
var font = argument6;
var textBorder = argument7;

if (text != "")
{
    // draw text
    draw_set_color(c_white);
    draw_set_font(font);
    draw_set_halign(fa_left);
    draw_set_valign(fa_top);
    draw_text(startX + border + textBorder, startY + border + textBorder, text);
}