///dialaogueRemoveAllSteppers()
// self-destruct all dialogue system steppers.

var current = ds_map_find_first(obj_dialogue.steppers);
var size = ds_map_size(obj_dialogue.steppers);
for (var i = 0; i < size; i++)
{
    var stepper = obj_dialogue.steppers[? current];
    // if it exists still
    if (instance_exists(stepper))
    {
        // self-destruct it
        stepper.remove = true;
    }
    current = ds_map_find_next(obj_dialogue.steppers, current);
}
// clear the map
ds_map_clear(obj_dialogue.steppers);