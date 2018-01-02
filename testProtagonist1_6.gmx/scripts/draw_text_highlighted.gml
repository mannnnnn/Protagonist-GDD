///draw_text_highlighted(x, y, str, col, alph, halign, valign)
var X = argument0;
var Y = argument1;
var str = argument2;
var col = argument3;
var alph = argument4;
var halign = argument5;
var valign = argument6;
if (str == "")
{
    return false;
}
var lines = ds_list_create();
string_split(str, newline(), lines);

var oldA = draw_get_alpha();
var oldC = draw_get_color();
draw_set_alpha(alph);
draw_set_color(col);

var totalh = string_height(str);
var h = totalh / ds_list_size(lines);
var filler = 10;

// draw rectangles
for (var i = 0; i < ds_list_size(lines); i++)
{
    var linetext = lines[| i];
    
    var w = string_width(linetext);
    
    var xoffset = 0;
    switch (halign)
    {
        case fa_left:
            xoffset = 0;
            break;
        case fa_center:
            xoffset = 0.5 * -w;
            break;
        case fa_right:
            xoffset = 1 * -w;
            break;
    }
    var yoffset = 0;
    switch (valign)
    {
        case fa_top:
            yoffset = (i * h) - 0;
            break;
        case fa_center:
            yoffset = (i * h) - (0.5 * totalh);
            break;
        case fa_bottom:
            yoffset = (i * h) - totalh;
            break;
    }
    draw_rectangle(X + xoffset - filler, Y + yoffset, X + xoffset + w, Y + yoffset + h, false);
}
ds_list_destroy(lines);

draw_set_alpha(oldA);
draw_set_color(oldC);
draw_set_halign(halign);
draw_set_valign(valign);
draw_text(X, Y, str);
return true;