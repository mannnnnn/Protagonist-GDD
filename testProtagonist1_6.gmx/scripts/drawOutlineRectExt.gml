///drawOutlineRectExt(x, y, w, h, border, colFill, colOutline, alphaFill, alphaOutline)
var X = argument0;
var Y = argument1;
var W = argument2;
var H = argument3;
var B = argument4;
var colFill = argument5;
var colOutline = argument6;
var alphaFill = argument7;
var alphaOutline = argument8;

// outline
draw_set_color(colOutline);
draw_set_alpha(alphaOutline);
// top bar
draw_rectangle(X, Y, X + W, Y + B, false);
// bottom bar
draw_rectangle(X, Y + H - B, X + W, Y + H, false);
// left bar
draw_rectangle(X, Y + B, X + B, Y + H - B, false);
// right bar
draw_rectangle(X + W - B, Y + B, X + W, Y + H - B, false);

// outline
draw_set_color(colFill);
draw_set_alpha(alphaFill);
draw_rectangle(X + B, Y + B, X + W - B, Y + H - B, false);

draw_set_alpha(1);