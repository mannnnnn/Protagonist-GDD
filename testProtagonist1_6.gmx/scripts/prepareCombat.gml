///prepareCombat(handler)
// handler is an object type, which is created.
// do what you need to do to set up combat.
// Called by the combat intro right after the room turns into the combat room.
obj_combat.handler = instance_create(0, 0, argument0);
obj_combat.testAllowed = true;
