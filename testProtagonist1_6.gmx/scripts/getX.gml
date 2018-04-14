///getX(inst)
// physics-safe position get
var inst = argument0;
if (object_get_physics(inst.object_index))
{
    return inst.phy_position_x;
}
return inst.x;
