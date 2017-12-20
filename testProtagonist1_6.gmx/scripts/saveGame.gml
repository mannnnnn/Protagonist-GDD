///saveGame(saveID)

var saveID = argument0;
show_debug_message("You saved to " + string(saveID));

// save image
saveThumbnailImage(saveID);

// generate json save string
var save = ds_map_create();
save[? "x"] = playerX();
save[? "y"] = playerY();

// save the data to file
path = getSavePath(saveID, getSaveDataFilename());
var f = file_text_open_write(path);
file_text_write_string(f, json_encode(save));
file_text_writeln(f);
file_text_close(f);