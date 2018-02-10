///draw_texture_tiled(texture, x, y, w, h, texW, texH, ?col, ?alpha)
// get texture using sprite_get_texture or background_get_texture

var tex = argument[0];
var X = argument[1];
var Y = argument[2];
var W = argument[3];
var H = argument[4];
var texW = argument[5];
var texH = argument[6];
var u = W / texW;
var v = H / texH;

var col = c_white;
if (argument_count >= 8)
{
    col = argument[7];
}
var alpha = 1;
if (argument_count >= 9)
{
    var alpha = argument[8];
}

texture_set_repeat(true);
draw_primitive_begin_texture(pr_trianglestrip, tex);
draw_vertex_texture_colour(X, Y, 0, 0, col, alpha);
draw_vertex_texture_colour(X + W, Y, u, 0, col, alpha);
draw_vertex_texture_colour(X, Y + H, 0, v, col, alpha);
draw_vertex_texture_colour(X + W, Y + H, u, v, col, alpha);
draw_primitive_end();
texture_set_repeat(false);

return true;
