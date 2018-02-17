///drawHealthBar(x1, y1, w, h, value1, value2, maximum)
var x1 = argument0;
var y1 = argument1;
var w = argument2;
var h = argument3;
var value1 = argument4;
var value2 = argument5;
var maximum = argument6;

draw_set_alpha(1);
draw_set_colour(c_red);
draw_rectangle(x1, y1, x1 + w, y1 + h, false);
draw_set_colour(c_black);
draw_rectangle(x1, y1, x1 + w, y1 + h, true);

draw_set_colour(make_color_rgb(255, 89, 0));
draw_rectangle(x1, y1, x1 + map_range(value2, 0, maximum, 0, w), y1 + h, false);
draw_set_colour(c_black);
draw_rectangle(x1, y1, x1 + map_range(value2, 0, maximum, 0, w), y1 + h, true);

draw_set_colour(c_lime);
draw_rectangle(x1, y1, x1 + map_range(value1, 0, maximum, 0, w), y1 + h, false);
draw_set_colour(c_black);
draw_rectangle(x1, y1, x1 + map_range(value1, 0, maximum, 0, w), y1 + h, true);
