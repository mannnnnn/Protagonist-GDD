///string_join(list, delim, ?a, ?b)
var list = argument[0];
var delim = argument[1];
var s = "";
var a = 0;
var b = ds_list_size(list);
if (argument_count >= 4)
{
    a = argument[2];
    b = argument[3];
}
for (var i = a; i < b; i++)
{
    s += string(list[| i]);
    if (i != b - 1)
    {
        s += delim;
    }
}
return s;