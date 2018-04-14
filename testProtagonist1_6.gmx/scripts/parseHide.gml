///parseHide(line, split)
// add sprite hide to drawer queue, to be flushed when the next with statement occurs.
var line = argument0;
var split = argument1;

// make sure enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": hide requires at least 1 argument: hide channel ?img ?x ?y ?xscale ?yscale ?angle ?alpha', true);
}

// if channel is dialogue
// then hide dialogue
if (split[| 1] == "dialogue")
{
    dialogueHide();
    // no need to pause
    ds_list_destroy(split);
    return false;
}

// default
var drawerQueue = ds_list_create();
drawerQueue[| DRAWER_QUEUE_TYPE] = DRAWER_QUEUE_TYPE_NORMAL;
drawerQueue[| DRAWER_QUEUE_ACTION] = DRAWER_QUEUE_ACTION_HIDE;
drawerQueue[| DRAWER_QUEUE_CHANNEL] = split[| 1];

// add to dialogue show queue
ds_list_add(obj_dialogue.drawerQueue, drawerQueue);

// no need to pause
ds_list_destroy(split);
return false;
