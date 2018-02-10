///drawSaveNameplate(x, y, w, h, border, textBorder, str, textalpha, alpha)
var X = argument0;
var Y = argument1;
var W = argument2;
var H = argument3;
var B = argument4;
var textB = argument5;
var text = argument6;
var textalpha = argument7;
var alpha = argument8;

drawButtonRect(X, Y, W - 6, H - 6, obj_buttons.button, c_white, c_white, alpha, alpha);
draw_set_font(fnt_save);
draw_set_halign(fa_left);
draw_set_valign(fa_top);
draw_set_color(c_white);
draw_set_alpha(textalpha);
draw_text_outlined(X + textB, Y + textB, text);
draw_set_alpha(1);
