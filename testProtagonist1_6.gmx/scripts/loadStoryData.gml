///loadStoryData(?file)
/// load story data map
var datafile = obj_storyData.file;
if (argument_count >= 1)
{
    datafile = argument[0];
}
var fname = getGlobalSavePath(datafile);
if (!file_exists(fname))
{
    return false;
}
var file = file_text_open_read(fname);
ds_map_destroy(obj_storyData.data);
obj_storyData.data = json_decode(file_text_read_string(file));
file_text_readln(file);
file_text_close(file);

return true;
