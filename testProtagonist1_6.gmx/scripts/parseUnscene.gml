///parseHide(line, split)
// add scene hide to drawer queue, to be flushed when the next with statement occurs.
var line = argument0;
var split = argument1;

// default
var drawerQueue = ds_list_create();
drawerQueue[| DRAWER_QUEUE_TYPE] = DRAWER_QUEUE_TYPE_SCENE;
drawerQueue[| DRAWER_QUEUE_ACTION] = DRAWER_QUEUE_ACTION_HIDE;
drawerQueue[| DRAWER_QUEUE_CHANNEL] = "bgscene";

// add to dialogue show queue
ds_list_add(obj_dialogue.drawerQueue, drawerQueue);

// no need to pause
ds_list_destroy(split);
return false;