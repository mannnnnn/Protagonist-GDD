///playerDamage(power)
var damage = argument0;
obj_combatPuzzle.playerDamaged += min(obj_combatPuzzle.playerHP, damage);
obj_combatPuzzle.playerHP = max(0, obj_combatPuzzle.playerHP - damage);
