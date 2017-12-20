///GUIToRoomY(guiY, view)
var guiY = argument[0];
// get which view
var view = 0;
if (argument_count >= 2)
{
    view = argument[1];
}
else
{
    return guiY;
}
// find coordinate for view if necessary
if (view_enabled && view_visible[view])
{
    return (guiY * (view_hview[view] / display_get_gui_height())) + view_yview[view];
}
return guiY;