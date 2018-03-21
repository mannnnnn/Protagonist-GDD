///endCombat()
obj_combat.active = false;
with (obj_combat.handler)
{
    instance_destroy();
}
endCombatAnimation();