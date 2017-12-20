///getPlayerStatTotal(stat)
// sums player stats and item stats.

// player stats
var sum = getPlayerStat(argument0);
// sum of item stats
for (var i = 0; i < ds_list_size(obj_inventory.inventory); i++)
{
    var item = obj_inventory.inventory[| i];
    var s = item[| ITEM_STATS];
    if (ds_exists(s, ds_type_list))
    {
        sum += getStat(s, argument0);
    }
}
return sum;