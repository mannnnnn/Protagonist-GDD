///GUIToRoomX(guiX, view)
var guiX = argument[0];
// get which view
var view = 0;
if (argument_count >= 2)
{
    view = argument[1];
}
else
{
    return guiX;
}
// find coordinate for view if necessary
if (view_enabled && view_visible[view])
{
    return (guiX * (view_wview[view] / display_get_gui_width())) + view_xview[view];
}
return guiX;