///draw_text_outlined(x, y, str)
var X = argument0;
var Y = argument1;
var str = argument2;

var oldC = draw_get_colour();

draw_set_color(c_black);
draw_text(X - 1, Y, str);
draw_text(X, Y - 1, str);
draw_text(X + 1, Y, str);
draw_text(X, Y + 1, str);

draw_set_color(oldC);
draw_text(X, Y, str);
