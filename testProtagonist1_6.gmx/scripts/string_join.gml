///string_join(list, delim)
var list = argument0;
var delim = argument1;
var s = "";
var count = ds_list_size(list);
for (var i = 0; i < count; i++)
{
    s += string(list[| i]);
    if (i != count - 1)
    {
        s += delim;
    }
}
return s;