///spellSearch(str)
var str = argument0;
// binary search on the spell sorted list
var list = obj_spellbook.spells;
var lo = 0;
var hi = ds_list_size(list) - 1;
while (lo <= hi)
{
    var mid = floor(lo + (hi - lo) / 2);
    var val = string_compare(str, list[| mid]);
    if (val == 0)
    {
       return mid;
    }
    // left side
    else if (val < 0)
    {
        hi = mid - 1;
    }
    // right side
    else
    {
        lo = mid + 1;
    }
}
return noone;
