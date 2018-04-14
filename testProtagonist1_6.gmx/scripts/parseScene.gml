///parseScene(line, split)
// add scene to drawer queue, to be flushed when the next with statement occurs.
var line = argument0;
var split = argument1;

// make sure enough args
if (ds_list_size(split) < 2)
{
    show_error('Error in line "' + string(line) + '": scene requires at least 1 argument: scene spritename ?img ?x ?y ?xscale ?yscale ?angle ?alpha', true);
}

// default
var drawerQueue = ds_list_create();
drawerQueue[| DRAWER_QUEUE_TYPE] = DRAWER_QUEUE_TYPE_SCENE;
drawerQueue[| DRAWER_QUEUE_ACTION] = DRAWER_QUEUE_ACTION_SHOW;
drawerQueue[| DRAWER_QUEUE_CHANNEL] = "bgscene";
drawerQueue[| DRAWER_QUEUE_SPRITENAME] = split[| 1];
drawerQueue[| DRAWER_QUEUE_IMAGE] = 0;
drawerQueue[| DRAWER_QUEUE_ALPHA] = 1;

if (ds_list_size(split) >= 4)
{
    drawerQueue[| DRAWER_QUEUE_IMAGE] = split[| 2];
    drawerQueue[| DRAWER_QUEUE_ALPHA] = split[| 3];
}

if (ds_list_size(split) == 3)
{
    drawerQueue[| DRAWER_QUEUE_IMAGE] = split[| 2];
}

// add to dialogue show queue
ds_list_add(obj_dialogue.drawerQueue, drawerQueue);

// no need to pause
ds_list_destroy(split);
return false;
