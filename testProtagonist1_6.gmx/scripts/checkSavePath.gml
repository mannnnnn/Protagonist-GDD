///checkSavePath(save)
// checks a save for existence
if !(file_exists(getSavePath(argument0, getSaveDataFilename())))
{
    return false;
}
if !(file_exists(getSavePath(argument0, getSaveImageFilename())))
{
    return false;
}
return true;