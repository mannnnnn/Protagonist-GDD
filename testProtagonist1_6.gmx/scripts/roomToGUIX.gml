///roomToGUIX(roomX)
// converts room coords to GUI coords.
var roomX = argument[0];
// get which view
var view = 0;
if (argument_count >= 2)
{
    view = argument[1];
}
else
{
    return roomX;
}
// find coordinate for view if necessary
if (view_enabled && view_visible[view])
{
    return (roomX - view_xview[view]) * (display_get_gui_width() / view_wview[view]);
}
return roomX;