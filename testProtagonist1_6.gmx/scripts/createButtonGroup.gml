///createButtonGroup(fill, leftTop, leftBottom, rightTop, rightBottom, hEdge, vEdge)
var button = ds_list_create();
button[| BUTTON_FILL] = argument0;
button[| BUTTON_LU] = argument1;
button[| BUTTON_LD] = argument2;
button[| BUTTON_RU] = argument3;
button[| BUTTON_RD] = argument4;
button[| BUTTON_HEDGE] = argument5;
button[| BUTTON_VEDGE] = argument6;
return button;
