///mouse_in_rect(x1, y1, w, h)
var x1 = min(argument0, argument0 + argument2);
var x2 = max(argument0, argument0 + argument2);
var y1 = min(argument1, argument1 + argument3);
var y2 = max(argument1, argument1 + argument3);
return point_in_rectangle(cursor_get_x(), cursor_get_y(), x1, y1, x2, y2);