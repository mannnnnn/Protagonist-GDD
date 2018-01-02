///dialogueAddStepper(stepper, channel)
var stepper = argument0;
var channel = argument1;
// remove the old one if necessary
if (ds_map_exists(obj_dialogue.steppers, channel) && instance_exists(obj_dialogue.steppers[? channel]))
{
    obj_dialogue.steppers[? channel].remove = true;
}
// register the new one
obj_dialogue.steppers[? channel] = stepper;
stepper.channel = channel;