///combatStartCast(x, y, action)
// when the cast begins
var X = argument0;
var Y = argument1;
var action = argument2;

// create spell effect
if (ds_map_exists(obj_spellbook.spelleffects, action))
{
    var eff = instance_create(0, 0, obj_spellbook.spelleffects[? action]);
    eff.targetX = X;
    eff.targetY = Y;
}
