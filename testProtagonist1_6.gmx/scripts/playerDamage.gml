///playerDamage(power)
var damage = argument0;

if (!instance_exists(obj_combatPuzzle))
{
    return 0;
}

obj_combatPuzzle.playerDamaged += min(obj_combatPuzzle.playerHP, damage);
obj_combatPuzzle.playerHP = max(0, obj_combatPuzzle.playerHP - damage);
if (damage > 0)
{
    playSound(SFX, snd_oof, false);
}

// if player is dead
if (obj_combatPuzzle.playerHP <= 0)
{
    instance_create(0, 0, obj_triggerPostSphinx);
    runCombat();
}
