///hadesSpecialTestExtravaganza()
// prepare for athena's test
// toggle spell input
if (!instance_exists(obj_combatCursor))
{
    instance_create(0, 0, obj_combatCursor);
    instance_create(298, 455, obj_spellInput);
    createHands();
    obj_combat.handler = obj_jokeTestAth;
}
else
{
    with (obj_combatCursor)
    {
        instance_destroy();
    }
    with (obj_spellInput)
    {
        instance_destroy();
    }
    with (obj_hand)
    {
        instance_destroy();
    }
    with (obj_jokeTestAth)
    {
        instance_destroy();
    }
}
