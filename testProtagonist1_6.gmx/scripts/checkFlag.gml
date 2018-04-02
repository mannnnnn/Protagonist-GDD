///checkFlag(key, ?def)
var def = false;
if (argument_count >= 2)
{
    def = argument[1];
}
if (!ds_map_exists(obj_storyData.data, argument[0]))
{
    return def;
}
return obj_storyData.data[? argument[0]];
