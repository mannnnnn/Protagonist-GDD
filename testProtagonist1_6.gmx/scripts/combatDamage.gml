///combatDamage(x, y, power, ?element)
var targeted = true;
var X = argument[0];
if (is_string(X))
{
    X = 0;
    targeted = false;
}
var Y = argument[1];
if (is_string(Y))
{
    Y = 0;
    targeted = false;
}
var pow = argument[2];
var element = noone;
if (argument_count >= 4)
{
    element = argument[3];
}
if (instance_exists(obj_enemy) && instance_exists(obj_combatPuzzle))
{
    if (instance_exists(obj_syntaxBoss))
    {
        var damage = 0;
        if (targeted)
        {
            // try attacking various parts
            if (collision_point(X, Y, obj_synHead, true, false))
            {
                // attack head
                damage = 1.2 * pow;
                obj_synHead.damaged = true;
                obj_synHead.damageTimer = obj_synHead.damageDur + damage;
            }
            else if (collision_point(X, Y, obj_synLeg2, true, false))
            {
                // attack leg2
                damage = pow;
                obj_synLeg2.damaged = true;
                obj_synLeg2.damageTimer = obj_synLeg2.damageDur + damage;
            }
            else if (collision_point(X, Y, obj_synLeg3, true, false))
            {
                // attack leg3
                damage = pow;
                obj_synLeg3.damaged = true;
                obj_synLeg3.damageTimer = obj_synLeg3.damageDur + damage;
            }
            else if (collision_point(X, Y, obj_synLeg4, true, false))
            {
                // attack leg4
                damage = pow;
                obj_synLeg4.damaged = true;
                obj_synLeg4.damageTimer = obj_synLeg4.damageDur + damage;
            }
            else if (collision_point(X, Y, obj_synLeg1, true, false))
            {
                with (obj_synLeg1)
                {
                    if (object_index == obj_synLeg1)
                    {
                        // attack leg1
                        damage = pow;
                        damaged = true;
                        damageTimer = damageDur + damage;
                    }
                }
            }
            else if (collision_point(X, Y, obj_synBody, true, false))
            {
                // attack body
                damage = pow;
                obj_synBody.damaged = true;
                obj_synBody.damageTimer = obj_synBody.damageDur + damage;
            }
            else if (collision_point(X, Y, obj_synTail, true, false))
            {
                // attack tail
                damage = 2 * pow;
                obj_synTail.damaged = true;
                obj_synTail.damageTimer = obj_synTail.damageDur + damage;
            }
        }
        else
        {
            damage = pow;
            with (obj_syntaxBoss)
            {
                damaged = true;
                damageTimer = damageDur + damage;
            }
        }
        obj_combatPuzzle.enemyDamaged += min(obj_combatPuzzle.enemyHP, damage);
        obj_combatPuzzle.enemyHP = max(0, obj_combatPuzzle.enemyHP - damage);
    }
}
return false;
