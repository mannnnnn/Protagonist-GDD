///setX(inst, val)
// physics-safe position set
var inst = argument0;
var val = argument1;
if (object_get_physics(inst.object_index))
{
    inst.phy_position_x = val;
}
inst.x = val;
