///cursor_get_x()
// get cursor X position, even if it is outside the window.
if (view_enabled)
{
    return (((display_mouse_get_x() - window_get_x()) + view_xview[0]) / (view_wview[0] / view_wport[0]));
}
else
{
    return (display_mouse_get_x() - window_get_x());
}