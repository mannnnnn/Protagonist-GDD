///depthify()
// set depth to how it would be in the 2D world
depth = room_height + (room_height - getY(id)) + (object_get_depth(object_index) * image_yscale);