///shineEffect(mouseX, w, gradientW, angle, middleColor, middleAlpha, outsideColor, outsideAlpha)
var mouseX = argument0;
var w = argument1 * 0.5;
var gradientW = argument2;
var angle = argument3;
var middleColor = argument4;
var middleAlpha = argument5;
var outsideColor = argument6;
var outsideAlpha = argument7;
var halfHeight = getDisplayRoomHeight() * 0.5;

var shift = halfHeight * tan(degtorad(angle));

draw_primitive_begin(pr_trianglelist);

// draw outside
draw_vertex_colour(mouseX + shift + w + gradientW, 0, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX - shift + w + gradientW, halfHeight * 2, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX + shift + w + gradientW,  halfHeight * 2, outsideColor, outsideAlpha);

draw_vertex_colour(mouseX + shift - w - gradientW, 0, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX - shift - w - gradientW, 0, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX - shift - w - gradientW,  halfHeight * 2, outsideColor, outsideAlpha);

// draw gradient
draw_vertex_colour(mouseX + shift + w, 0, middleColor, middleAlpha);
draw_vertex_colour(mouseX + shift + w + gradientW, 0, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX - shift + w,  halfHeight * 2, middleColor, middleAlpha);

draw_vertex_colour(mouseX + shift + w + gradientW, 0, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX - shift + w, halfHeight * 2, middleColor, middleAlpha);
draw_vertex_colour(mouseX - shift + w + gradientW,  halfHeight * 2, outsideColor, outsideAlpha);

draw_vertex_colour(mouseX + shift - w, 0, middleColor, middleAlpha);
draw_vertex_colour(mouseX + shift - w - gradientW, 0, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX - shift - w,  halfHeight * 2, middleColor, middleAlpha);

draw_vertex_colour(mouseX + shift - w - gradientW, 0, outsideColor, outsideAlpha);
draw_vertex_colour(mouseX - shift - w, halfHeight * 2, middleColor, middleAlpha);
draw_vertex_colour(mouseX - shift - w - gradientW,  halfHeight * 2, outsideColor, outsideAlpha);

// draw middle
draw_vertex_colour(mouseX + shift + w, 0, middleColor, middleAlpha);
draw_vertex_colour(mouseX + shift - w, 0, middleColor, middleAlpha);
draw_vertex_colour(mouseX - shift + w,  halfHeight * 2, middleColor, middleAlpha);

draw_vertex_colour(mouseX + shift - w, 0, middleColor, middleAlpha);
draw_vertex_colour(mouseX - shift - w, halfHeight * 2, middleColor, middleAlpha);
draw_vertex_colour(mouseX - shift + w,  halfHeight * 2, middleColor, middleAlpha);
draw_primitive_end();

// draw edges
draw_set_color(outsideColor);
draw_set_alpha(outsideAlpha);
draw_rectangle(mouseX + shift + w + gradientW, 0, getDisplayRoomWidth(), halfHeight * 2, false);
draw_rectangle(mouseX - shift - w - gradientW - 1, 0, 0, halfHeight * 2, false);
