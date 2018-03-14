///combatLastCast(x, y, action)
// when the last letter hits
var X = argument0;
var Y = argument1;
var action = argument2;

// tell the handler about this
if (instance_exists(obj_combat.handler))
{
    obj_combat.handler.X = X;
    obj_combat.handler.Y = Y;
    obj_combat.handler.action = action;
    with (obj_combat.handler)
    {
        event_user(SPELLCAST_LASTHIT);
    }
}