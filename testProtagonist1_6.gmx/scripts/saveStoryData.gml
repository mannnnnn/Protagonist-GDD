/// save story data map
var fname = getGlobalSavePath(obj_storyData.file);
var file = file_text_open_write(fname);
file_text_write_string(file, json_encode(obj_storyData.data));
file_text_writeln(file);
file_text_close(file);
