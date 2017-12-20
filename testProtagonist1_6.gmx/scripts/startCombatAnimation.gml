///startCombatAnimation()
// play the animation that marks the beginning of combat.
var c = instance_create(0, 0, obj_combatIntro);
obj_combat.active = true;
return c;
