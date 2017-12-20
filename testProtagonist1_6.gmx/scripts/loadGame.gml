///loadGame(saveID)

var saveID = argument0;
show_debug_message("You loaded from " + string(saveID));

// get json save string
path = getSavePath(saveID, getSaveDataFilename());
var f = file_text_open_read(path);
var json = file_text_read_string(f);
file_text_close(f);
var data = json_decode(json);

// load data into the game
setX(obj_protagonist, data[? "x"]);
setY(obj_protagonist, data[? "y"]);

ds_map_destroy(data);