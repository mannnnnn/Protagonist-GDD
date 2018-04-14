///printLabels(labels)
var map = argument0;

var current = ds_map_find_first(map);
var size = ds_map_size(map);

for (var i = 0; i < size; i++)
{
    var label = map[? current];
    show_debug_message(label[| LABEL_NAME]);
    show_debug_message(label[| LABEL_LINE]);
    show_debug_message(label[| LABEL_TYPE]);
    show_debug_message("");
    current = ds_map_find_next(map, current);
}
