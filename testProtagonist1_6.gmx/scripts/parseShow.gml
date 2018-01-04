///parseShow(line, split)
// add sprite to drawer queue, to be flushed when the next with statement occurs.
var line = argument0;
var split = argument1;

// if channel is dialogue
// then show dialogue
if (ds_list_size(split) >= 2 && split[| 1] == "dialogue")
{
    dialogueUnhide();
    // no need to pause
    ds_list_destroy(split);
    return false;
}

// make sure enough args
if (ds_list_size(split) < 3)
{
    show_error('Error in line "' + string(line) + '": show requires at least 2 arguments: show channel spritename ?img ?x ?y ?xscale ?yscale ?angle ?alpha', true);
}

// default
var drawerQueue = ds_list_create();
drawerQueue[| DRAWER_QUEUE_TYPE] = DRAWER_QUEUE_TYPE_NORMAL;
drawerQueue[| DRAWER_QUEUE_ACTION] = DRAWER_QUEUE_ACTION_SHOW;
drawerQueue[| DRAWER_QUEUE_CHANNEL] = split[| 1];
drawerQueue[| DRAWER_QUEUE_SPRITENAME] = split[| 2];
drawerQueue[| DRAWER_QUEUE_IMAGE] = 0;
drawerQueue[| DRAWER_QUEUE_POS_X] = 0;
drawerQueue[| DRAWER_QUEUE_POS_Y] = 0;
drawerQueue[| DRAWER_QUEUE_SCALE_X] = 1;
drawerQueue[| DRAWER_QUEUE_SCALE_Y] = 1;
drawerQueue[| DRAWER_QUEUE_ANGLE] = 0;
drawerQueue[| DRAWER_QUEUE_ALPHA] = 1;

// get data from args
for (var i = 3; i < ds_list_size(split) && i + 1 < ds_list_size(drawerQueue); i++)
{
    drawerQueue[| i + 1] = real(split[| i]);
}

// add to dialogue show queue
ds_list_add(obj_dialogue.drawerQueue, drawerQueue);

// no need to pause
ds_list_destroy(split);
return false;
