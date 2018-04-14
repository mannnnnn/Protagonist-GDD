///createParticleTypeArc(sys)
// Creates the seaBack particle type and returns it.
var part = part_type_create();
part_type_shape(part, pt_shape_pixel);
part_type_scale(part, 1, 1);
part_type_size(part, 0.01, 0.50, 0.01, 0.40);
part_type_speed(part, 0, 0, 0, 0);
part_type_direction(part, 146, 401, 0, 0);
part_type_orientation(part, 0, 360, 0, 0, false);
part_type_gravity(part, 0.70, 90);
part_type_colour3(part, c_white, c_yellow, c_black);
part_type_alpha2(part, 0, 1);
part_type_blend(part, true);
part_type_life(part, 50, 80);
return part;
