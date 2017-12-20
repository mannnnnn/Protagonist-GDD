///createRoomTransition(room, ?obj, ?duration)
// will create a transition object that changes to the target room.
// enter noone for the room argument if you only want the animation without a room change.

var target = argument[0];

// default to fade transition, but accept any transition obj type
var obj = obj_fadeTransition;
if (argument_count >= 2)
{
    obj = argument[1];
}
// type check
if !(objectof(obj, obj_roomTransition))
{
    show_error("Error: cannot create the room transition of type " + object_get_name(obj) + ", since it isn't a child of obj_roomTransition.", true);
}

// create the transition object
var trans = instance_create(0, 0, obj);
trans.target = target;

// set duration if argument exists
var duration = trans.duration;
if (argument_count >= 3)
{
    duration = argument[2];
}
trans.duration = duration;

return trans;

// creating more room transitions may require shaders...
// google xor shaders for some good tutorials on these.