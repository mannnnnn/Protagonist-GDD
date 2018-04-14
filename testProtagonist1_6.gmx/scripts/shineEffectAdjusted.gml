///shineEffectAdjusted()
// draws a shine effect moved to mouse position
shineEffect(cursor_get_x() + ((cursor_get_y() - (getDisplayRoomHeight() * 0.5)) * tan(degtorad(45))), room_width / 8, room_width / 16, 45, c_black, 0, c_black, 0.5);
