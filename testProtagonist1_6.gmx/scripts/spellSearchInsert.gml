///spellSearchInsert(str)
var str = argument0;
// binary search on the spell sorted list, for insertion only
var list = obj_spellbook.spells;
if (ds_list_empty(list))
{
    return 0;
}
// binary search
var lo = 0;
var hi = ds_list_size(list);
while (lo < hi)
{
    var mid = floor((lo + hi) / 2);
    var val = string_compare(str, list[| mid]);
    if (val == 0)
    {
        return noone;
    }
    if (val > 0)
    {
        lo = mid + 1;
    }
    else
    {
        hi = mid;
    }
}
return lo;