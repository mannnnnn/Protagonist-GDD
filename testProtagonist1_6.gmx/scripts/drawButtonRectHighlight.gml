///drawButtonRectHighlight(x, y, w, h, buttonGroup, colFill, colOutline, alphaFill, alphaOutline, highlightColor, highlightAlpha)
var X = round(argument0);
var Y = round(argument1);
var W = round(argument2);
var H = round(argument3);
if (W < 0)
{
    X += W;
    W = abs(W);
}
if (H < 0)
{
    Y += H;
    H = abs(H);
}
var button = argument4;
var colFill = argument5;
var colOutline = argument6;
var alphaFill = argument7;
var alphaOutline = argument8;
var highlightColor = argument9;
var highlightAlpha = argument10;

draw_set_alpha(1);

// fill
var spr = button[| BUTTON_FILL];
var tex = sprite_get_texture(spr, 0);
draw_texture_tiled(tex, X, Y, W, H, sprite_get_width(spr), sprite_get_height(spr), colFill, alphaFill);

// highlight
draw_set_alpha(highlightAlpha);
draw_set_color(highlightColor);
draw_rectangle(X, Y, X + W, Y + H, false);

draw_set_alpha(1);
// border
// horizontal edges
var spr = button[| BUTTON_HEDGE];
var tex = sprite_get_texture(spr, 0);
draw_texture_tiled(tex, X, Y, W, sprite_get_height(spr), sprite_get_width(spr), sprite_get_height(spr), colOutline, alphaOutline);
draw_texture_tiled(tex, X, Y + H, W, sprite_get_height(spr), sprite_get_width(spr), sprite_get_height(spr), colOutline, alphaOutline);
// vertical edges
var spr = button[| BUTTON_VEDGE];
var tex = sprite_get_texture(spr, 0);
draw_texture_tiled(tex, X, Y, sprite_get_width(spr), H, sprite_get_width(spr), sprite_get_height(spr), colOutline, alphaOutline);
draw_texture_tiled(tex, X + W, Y, sprite_get_width(spr), H, sprite_get_width(spr), sprite_get_height(spr), colOutline, alphaOutline);
// the four corners
draw_sprite_ext(button[| BUTTON_LU], 0, X, Y, 1, 1, 0, colOutline, alphaOutline);
draw_sprite_ext(button[| BUTTON_LD], 0, X, Y + H, 1, 1, 0, colOutline, alphaOutline);
draw_sprite_ext(button[| BUTTON_RU], 0, X + W, Y, 1, 1, 0, colOutline, alphaOutline);
draw_sprite_ext(button[| BUTTON_RD], 0, X + W, Y + H, 1, 1, 0, colOutline, alphaOutline);