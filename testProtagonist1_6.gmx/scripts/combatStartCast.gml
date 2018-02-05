///combatStartCast(x, y, action)
// when the cast begins
var X = argument0;
var Y = argument1;
var action = argument2;

// create spell effect
if (ds_map_exists(obj_spellbook.spelleffects, action))
{
    var obj = obj_spellbook.spelleffects[? action];
    if (object_exists(obj))
    {
        var eff = instance_create(0, 0, obj);
        eff.targetX = X;
        eff.targetY = Y;
    }
}

// tell the handler about this
if (instance_exists(obj_combat.handler))
{
    obj_combat.handler.X = X;
    obj_combat.handler.Y = Y;
    obj_combat.handler.action = action;
    with (obj_combat.handler)
    {
        event_user(SPELLCAST_START);
    }
}

