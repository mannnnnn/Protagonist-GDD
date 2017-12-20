///drawDialogueRect(x, y, w, h, border)
var X = argument0;
var Y = argument1;
var W = argument2;
var H = argument3;
var B = argument4;

draw_set_color(c_white);
draw_rectangle(X, Y, X + W, Y + H, false);

draw_set_color(c_black);
draw_rectangle(X + B, Y + B, X + W - B, Y + H - B, false);