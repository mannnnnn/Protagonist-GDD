///cursor_get_y()
// get cursor Y position, even if it is outside the window.
if (view_enabled)
{
    return (((display_mouse_get_y() - window_get_y()) + view_yview[0]) / (view_hview[0] / view_hport[0]));
}
else
{
    return (display_mouse_get_y() - window_get_y());
}