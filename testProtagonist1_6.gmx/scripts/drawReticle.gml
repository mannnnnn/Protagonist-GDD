///drawReticle(X, Y, w1, thick1, w2, thick2, ang, linew1, linew2, linethick, x2, y2)

var X = argument0;
var Y = argument1;
var w1 = argument2;
var thick1 = argument3;
var w2 = argument4;
var thick2 = argument5;
var angle = argument6;
var linew1 = argument7;
var linew2 = argument8;
var linethick = argument9;
var X2 = argument10;
var Y2 = argument11;

// red circle
draw_ring(X, Y, w1, thick1, 2 * pi * w1 / 5, c_red, 1);
// draw lines around the circle
draw_set_color(c_red);
draw_set_alpha(1);
for (var i = angle; i < angle + 360; i += 90)
{
    draw_line_width(X + lengthdir_x(linew1, i), Y + lengthdir_y(linew1, i), 
    X + lengthdir_x(linew2, i), Y + lengthdir_y(linew2, i), linethick);
}
// draw inner circle
draw_ring(X2, Y2, w2, thick2, 2 * pi * w2 / 5, c_orange, 1);
