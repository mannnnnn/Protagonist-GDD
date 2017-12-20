///roomToGUIY(roomY)
// converts room coords to GUI coords.
var roomY = argument[0];
// get which view
var view = 0;
if (argument_count >= 2)
{
    view = argument[1];
}
else
{
    return roomY;
}
// find coordinate for view if necessary
if (view_enabled && view_visible[view])
{
    return (roomY - view_yview[view]) * (display_get_gui_height() / view_hview[view]);
}
return roomY;