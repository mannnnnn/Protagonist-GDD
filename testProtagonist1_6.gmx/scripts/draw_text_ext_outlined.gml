///draw_text_ext_outlined(x, y, str, sep, w)
var X = argument0;
var Y = argument1;
var str = argument2;
var sep = argument3;
var w = argument4;

var oldC = draw_get_colour();

draw_set_color(c_black);
draw_text_ext(X - 1, Y, str, sep, w);
draw_text_ext(X, Y - 1, str, sep, w);
draw_text_ext(X + 1, Y, str, sep, w);
draw_text_ext(X, Y + 1, str, sep, w);

draw_set_color(oldC);
draw_text_ext(X, Y, str, sep, w);
