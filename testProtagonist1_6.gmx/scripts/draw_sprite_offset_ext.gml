///draw_sprite_offset_ext(spr, img, x, y, xoffset, yoffset, xscale, yscale, rot, col, alpha)
// draw_sprite_ext with custom offset. To shift offset, use sprite_get_xoffset(spr) + xoffset

var spr = argument0;
var img = argument1;
var X = argument2;
var Y = argument3;
var xoffset = argument4;
var yoffset = argument5;
var xscale = argument6;
var yscale = argument7;
var rot = argument8;
var col = argument9;
var alpha = argument10;

var rx = sprite_get_xoffset(spr);
var ry = sprite_get_yoffset(spr);
var w = sprite_get_width(spr);
var h = sprite_get_height(spr);
// unrotated origin shift
var adjX = (rx - xoffset) * xscale;
var adjY = (ry - yoffset) * yscale;
var mag = point_distance(0, 0, adjX, adjY);
var ang = point_direction(0, 0, adjX, adjY);
// draw at correct position
draw_sprite_ext(spr, img, X + lengthdir_x(mag, ang + rot), Y + lengthdir_y(mag, ang + rot), xscale, yscale, rot, col, alpha);