///createRoomTransition(room, ?obj, ?movePlayer, ?duration)
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

// get player position and record it
if (instance_exists(obj_protagonist))
{
    obj_map.posX = playerX();
    obj_map.posY = playerY();
}
// tell obj_map where to put the player in the new room
obj_map.movePlayer = false;
if (argument_count >= 3)
{
    obj_map.movePlayer = argument[2];
}

// create the transition object
var trans = instance_create(0, 0, obj);
trans.target = target;

// set duration if argument exists
var duration = trans.duration;
if (argument_count >= 4)
{
    duration = argument[3];
}
trans.duration = duration;

// transfer help data from obj_map to this
if (obj_map.help && (obj_map.helpTarget == noone || obj_map.helpTarget == target))
{
    trans.pause = true;
    trans.pauseSprite = obj_map.helpSpr;
    trans.pauseImage = obj_map.helpImg;
    obj_map.help = false;
}

return trans;
