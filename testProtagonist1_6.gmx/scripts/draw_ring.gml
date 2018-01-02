///draw_ring(X, Y, r, thickness, prec, col, alpha);
var X = argument0;
var Y = argument1;
var r = argument2;
var thickness = argument3;
var prec = argument4;
var jadd = 360 / prec;
var col = argument5;
var alpha = argument6;
draw_set_color(col);
draw_set_alpha(alpha);
draw_primitive_begin(pr_trianglestrip);
for (var j = 0; j <= 360 + jadd; j += jadd)
{
    draw_vertex(X + lengthdir_x(r, j), Y + lengthdir_y(r, j));
    draw_vertex(X + lengthdir_x(r + thickness, j), Y + lengthdir_y(r + thickness, j));
}
draw_primitive_end();