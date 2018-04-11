///mapToStr(map)
var map = argument0;
var str = "";
var i = 0;
for (var key = ds_map_find_first(map); i < ds_map_size(map); key = ds_map_find_next(map, key))
{
    i++;
    var keyStr = string(key);
    if (is_string(key))
    {
        keyStr = '"' + keyStr + '"';
    }
    str += newline() + keyStr + ": " + string(map[? key]);
}
return str;
